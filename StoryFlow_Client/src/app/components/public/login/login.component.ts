import { Component, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/_services/user.service';
import { LoginDto } from 'src/app/models/login-dto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
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
  ) {}
  changePasswordVisibility(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }
  login() {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let dto: LoginDto = {
      email: this.form.get('email').value,
      password: this.form.get('password').value,
    };

    this.userService.login(dto).subscribe({
      next: () => {
        this.toastr.success("Logged in successfully.");
        this.router.navigateByUrl('text-selection');
      },
      error: (error) => {
        if (error.status === 400) {
          this.toastr.error(error.error);
          this.form.reset();
        }
        console.log(error);
      },
    });
  }
}
