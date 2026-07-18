import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/_services/loader.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { QuizService } from 'src/app/_services/quiz.service';
import { UserService } from 'src/app/_services/user.service';
import { GENDERS } from 'src/app/constants/select-options';
import { LoginDto } from 'src/app/models/login-dto';
import { RegisterUserDto } from 'src/app/models/register-user-dto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  hidePassword = signal(true);
  hideRepeatPassword = signal(true);

  genders = GENDERS;
  pendingQuiz: string | null = null;

  form: any = this.fb.group({
    firstName: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(25)],
    ],
    lastName: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(25)],
    ],
    dateOfBirth: ['', Validators.required],
    gender: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: [
      '',
      [Validators.required, Validators.minLength(5), Validators.maxLength(25)],
    ],
    repeatPassword: [
      '',
      [Validators.required, Validators.minLength(5), Validators.maxLength(25)],
    ],
  });

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private userService: UserService,
    public loaderService: LoaderService,
    private navbarService: NavbarService,
    private quizService: QuizService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loaderService.hide();

    this.pendingQuiz = localStorage.getItem('pendingQuiz');
  }

  changePasswordVisibility(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }

  changeRepeatPasswordVisibility(event: MouseEvent) {
    this.hideRepeatPassword.set(!this.hideRepeatPassword());
    event.stopPropagation();
  }

  register() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let dto: RegisterUserDto = {
      email: this.form.get('email').value,
      password: this.form.get('password').value,
      repeatPassword: this.form.get('repeatPassword').value,
      firstName: this.form.get('firstName').value,
      lastName: this.form.get('lastName').value,
      gender: this.form.get('gender').value,
      dateOfBirth: this.form
        .get('dateOfBirth')
        .value.toISOString()
        .split('T')[0],
    };

    this.navbarService.blockButtons();

    this.userService.register(dto).subscribe({
      next: () => {
        console.log('Zarejestrowano pomyślnie');

        this.login();
      },
      error: (error) => {
        this.toastr.error(error.error);
        this.loaderService.hide();
        this.navbarService.unblockButtons();
        this.form.reset();
      },
    });
  }

  login() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const dto: LoginDto = {
      email: this.form.get('email')?.value,
      password: this.form.get('password')?.value,
    };

    this.navbarService.blockButtons();
    this.loaderService.show();

    this.userService.login(dto).subscribe({
      next: (response) => {
        if (!response.token) {
          this.loaderService.hide();
          this.navbarService.unblockButtons();
          return;
        }

        console.log('success');
        this.loaderService.hide();
        this.toastr.success('Zalogowano pomyślnie.');

        if (this.pendingQuiz) {
          this.sendAnswers(this.pendingQuiz);
        }

        this.router.navigateByUrl('sezon-selection');
      },

      error: (error) => {
        if (error.status !== 400) {
          this.loaderService.hide();
          this.navbarService.unblockButtons();
          this.toastr.error('Błąd serwera.');
        }

        if (error.status === 400) {
          console.log('error');
          this.toastr.error('Błędne dane logowania.');
          this.loaderService.hide();
          this.navbarService.unblockButtons();
          this.form.reset();
        }
      },
    });
  }

  sendAnswers(pendingQuiz: string) {
    const quiz = JSON.parse(pendingQuiz);

    this.quizService.sendAnswers(quiz).subscribe({
      next: (response) => {
        console.log(response);
        console.log('remove pending quiz:');
        localStorage.removeItem('pendingQuiz');
      },
      error: (error) => console.log(error),
    });
  }
}
