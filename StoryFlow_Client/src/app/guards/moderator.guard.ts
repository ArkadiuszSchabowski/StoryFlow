import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { combineLatest, map, take } from 'rxjs';
import { AuthService } from '../_services/auth.service';

export const moderatorGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  return combineLatest([
    authService.currentUserSource$,
    authService.isModerator$,
    authService.isAdmin$,
  ]).pipe(
    take(1),
    map(([token, isModerator, isAdmin]) => {
      if (!token) {
        toastr.error('Zaloguj się, by przejść na tę stronę.');
        router.navigateByUrl('/login');
        return false;
      }

      if (isModerator || isAdmin) {
        return true;
      }

      toastr.error('Nie masz uprawnień, by wykonać tę akcję.');
      router.navigateByUrl('/sezon-selection');
      return false;
    })
  );
};