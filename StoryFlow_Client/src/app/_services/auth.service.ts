import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSource = new BehaviorSubject<string | null>(null);
  currentUserSource$ = this.currentUserSource.asObservable();

 public isModerator$ = this.currentUserSource$.pipe(
    map(token => {
      if (!token) return false;

      const payload = JSON.parse(atob(token.split('.')[1]));

      const role =
        payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      return role?.toLowerCase() === 'moderator';
    })
  );

   public isAdmin$ = this.currentUserSource$.pipe(
    map(token => {
      if (!token) return false;

      const payload = JSON.parse(atob(token.split('.')[1]));

      const role =
        payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      return role?.toLowerCase() === 'administrator';
    })
  );

  constructor() {}

  getToken(): string | null {
    const token = localStorage.getItem('token');

    if (token === null) {
      return null;
    }

    return token;
  }

  setUser(token: string) {
    localStorage.setItem('token', token);
    this.currentUserSource.next(token);
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }
}
