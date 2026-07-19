import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
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
    nick: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(25)],
    ],
    dateOfBirth: [new Date(2000, 0, 15), Validators.required],
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
    private authService: AuthService,
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
      nick: this.form.get('nick').value,
      gender: this.form.get('gender').value,
      dateOfBirth: this.form
        .get('dateOfBirth')
        .value.toISOString()
        .split('T')[0],
    };

    this.userService.register(dto).subscribe({
      next: () => {
        this.login();
      },
      error: (error) => {
        this.toastr.error(error.error);
        this.loaderService.hide();
        this.navbarService.unblockButtons();
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

    const start = Date.now();
    const minTime = 2000;

    this.userService.login(dto).subscribe({
      next: (response) => {
        const elapsedTime = Date.now() - start;
        const remaining = Math.max(0, minTime - elapsedTime);

        setTimeout(() => {
          if (response.token) {
            this.loaderService.hide();
            this.navbarService.unblockButtons();
            this.authService.setUser(response.token);
            this.toastr.success('Zalogowano pomyślnie.');

            if (this.pendingQuiz) {
              this.sendAnswers(this.pendingQuiz);
            }

            this.router.navigateByUrl('sezon-selection');
          }
        }, remaining);
      },

      error: (error) => {
        if (error.status !== 400) {
          this.loaderService.hide();
          this.navbarService.unblockButtons();
          this.toastr.error('Błąd serwera.');
        }

        if (error.status === 400) {
          this.toastr.error('Błędne dane logowania.');
          this.loaderService.hide();
          this.navbarService.unblockButtons();
        }
      },
    });
  }

  sendAnswers(pendingQuiz: string) {
    const quiz = JSON.parse(pendingQuiz);

    this.quizService.sendAnswers(quiz).subscribe({
      next: () => {
        localStorage.removeItem('pendingQuiz');
      },
      error: (error) => console.log(error),
    });
  }
}
