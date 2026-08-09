import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

export const guestGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const token = authService.currentUserSource.getValue();
  if (!token) {
    return true;
  } else {
    router.navigateByUrl('/sezon-selection');
    return false;
  }
};