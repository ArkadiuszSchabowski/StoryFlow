import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { LoaderService } from 'src/app/_services/loader.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { QuizService } from 'src/app/_services/quiz.service';
import { UserService } from 'src/app/_services/user.service';
import { LoginDto } from 'src/app/models/login-dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  hidePassword = signal(true);
  form: any = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
  });

  pendingQuiz: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
    public authService: AuthService,
    public loaderService: LoaderService,
    private navbarService: NavbarService,
    private quizService: QuizService,
  ) {}

  ngOnInit(): void {
    this.loaderService.hide();

    this.pendingQuiz = localStorage.getItem('pendingQuiz');
  }

  changePasswordVisibility(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
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
          this.form.reset();
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
