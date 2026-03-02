import { Component, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/_services/user.service';
import { RegisterUserDto } from 'src/app/models/register-user-dto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  hidePassword = signal(true);
  hideRepeatPassword = signal(true);

  genders = [
    { value: 0, viewValue: 'Female' },
    { value: 1, viewValue: 'Male' },
  ];

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
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
  ) {}

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
      dateOfBirth: this.form.get('dateOfBirth').value,
    };

    this.userService.register(dto).subscribe({
      next: (response) => {
        console.log(response);
        this.toastr.success('Registered successfully.');
        this.router.navigateByUrl('login');
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
