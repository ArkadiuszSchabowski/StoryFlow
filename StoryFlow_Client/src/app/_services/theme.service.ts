import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserService } from './user.service';
import { AuthService } from './auth.service';
import { GetUserDto } from '../models/get-user-dto';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  themeSubject = new BehaviorSubject<boolean>(false);
  theme$ = this.themeSubject.asObservable();
  user: GetUserDto | null = null;

  constructor(
    private userService: UserService,
    private authService: AuthService,
  ) {
    this.authService.currentUserSource$.subscribe((token) => {
      if (token) {
        this.getProfile();
      }
    });
  }

  getProfile() {
    this.userService.getProfile().subscribe({
      next: (response) => {
        this.user = response;
        this.getTheme(response.userPreferences.isLightTheme);
      },
      error: (error) => console.log(error),
    });
  }

  getTheme(isLightTheme: boolean) {
    this.themeSubject.next(isLightTheme);
  }
}