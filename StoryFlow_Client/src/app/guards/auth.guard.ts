import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  const token = authService.currentUserSource.getValue();
  if (token) {
    return true;
  } else {
    toastr.error('Zaloguj się, by przejść na tę stronę.');
    router.navigateByUrl('/login');
    return false;
  }
};