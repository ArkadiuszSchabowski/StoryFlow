import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';

export const guestGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
    const toastr = inject(ToastrService);
  const router = inject(Router);

  const token = authService.currentUserSource.getValue();
  if (!token) {
    return true;
  } else {
    toastr.error('Strona, na którą próbowałeś przejść, jest dostępna tylko dla niezalogowanych.');
    router.navigateByUrl('/sezon-selection');
    return false;
  }
};