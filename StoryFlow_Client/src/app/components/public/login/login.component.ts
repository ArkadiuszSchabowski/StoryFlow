import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { LoaderService } from 'src/app/_services/loader.service';
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

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
    public authService: AuthService,
    public loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.loaderService.hide();
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

  this.loaderService.show();

  const start = Date.now();
  const minTime = 3000;

  this.userService.login(dto).subscribe({
    next: (response) => {
      if (!response.token) {
        this.loaderService.hide();
        return;
      }

      const elapsed = Date.now() - start;
      const remaining = Math.max(0, minTime - elapsed);

      setTimeout(() => {
        this.loaderService.hide();
        this.toastr.success('Zalogowano pomyślnie.');
        this.router.navigateByUrl('text-selection');
      }, remaining);
    },

    error: (error) => {
      const elapsed = Date.now() - start;
      const remaining = Math.max(0, minTime - elapsed);

      setTimeout(() => {
        this.loaderService.hide();

        if (error.status === 400) {
          this.toastr.error('Błędne dane logowania.');
          this.form.reset();
        }
      }, remaining);
    },
  });
}
}
