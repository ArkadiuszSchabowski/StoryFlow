import { Component, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  hidePassword = signal(true);
  hideRepeatPassword = signal(true);
  form: any = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
    repeatPassword: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
  });

  constructor(private fb: FormBuilder) {}

  changePasswordVisibility(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }

  changeRepeatPasswordVisibility(event: MouseEvent) {
    this.hideRepeatPassword.set(!this.hideRepeatPassword());
    event.stopPropagation();
  }

  register() {
    console.log(this.form);
  }
}
