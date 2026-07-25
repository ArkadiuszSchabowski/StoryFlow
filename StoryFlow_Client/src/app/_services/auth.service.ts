import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSource = new BehaviorSubject<string | null>(null);
  currentUserSource$ = this.currentUserSource.asObservable();
  private isBrowser: boolean;

  constructor(@Inject(PLATFORM_ID) platformId: Object) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  public isModerator$ = this.currentUserSource$.pipe(
    map((token) => {
      if (!token) return false;

      const payload = JSON.parse(atob(token.split('.')[1]));

      const role =
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      return role?.toLowerCase() === 'moderator';
    }),
  );

  public isAdmin$ = this.currentUserSource$.pipe(
    map((token) => {
      if (!token) return false;

      const payload = JSON.parse(atob(token.split('.')[1]));

      const role =
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      return role?.toLowerCase() === 'administrator';
    }),
  );

  getToken(): string | null {
    if (!this.isBrowser) {
      return null;
    }

    const token = localStorage.getItem('token');

    if (token === null) {
      return null;
    }

    return token;
  }

  initializeUser() {
    if (!this.isBrowser) {
      return;
    }

    const token = localStorage.getItem('token');
    this.currentUserSource.next(token);
  }

  setUser(token: string) {
    if (!this.isBrowser) {
      return;
    }

    localStorage.setItem('token', token);
    this.currentUserSource.next(token);
  }

  logout() {
    if (!this.isBrowser) {
      return;
    }

    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }
}
