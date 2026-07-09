import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/_services/loader.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { UserService } from 'src/app/_services/user.service';
import { GENDERS } from 'src/app/constants/select-options';
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
    public loaderService: LoaderService,
    private navbarService: NavbarService,
  ) {}

  ngOnInit(): void {
    this.loaderService.hide();
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
    this.loaderService.show();

    const start = Date.now();
    const minTime = 10000;

    this.userService.register(dto).subscribe({
      next: () => {
        const elapsed = Date.now() - start;
        const remaining = Math.max(0, minTime - elapsed);

        setTimeout(() => {
          this.loaderService.hide();
          this.navbarService.unblockButtons();
          this.toastr.success('Zarejestrowano pomyślnie.');
          this.router.navigateByUrl('login');
        }, remaining);
      },
      error: (error) => {
        this.toastr.error(error.error);
        this.loaderService.hide();
        this.navbarService.unblockButtons();
        this.form.reset();
      },
    });
  }
}
