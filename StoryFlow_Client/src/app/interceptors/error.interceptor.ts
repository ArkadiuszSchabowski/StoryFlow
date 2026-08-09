import {
  HttpErrorResponse,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, of, retry, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

const RETRYABLE_STATUSES = [0, 502, 503, 504];

function isRetryable(req: HttpRequest<unknown>, status: number): boolean {
  return req.method === 'GET' && RETRYABLE_STATUSES.includes(status);
}

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastr = inject(ToastrService);
  const router = inject(Router);

  return next(req).pipe(
    retry({
      count: 2,
      delay: (error: HttpErrorResponse) =>
        isRetryable(req, error.status) ? of(null) : throwError(() => error),
    }),
    catchError((error: HttpErrorResponse) => {
      switch (error.status) {
        case 0:
          toastr.error(
            'Nie udało się połączyć z serwerem. Może się on właśnie uruchamiać — spróbuj ponownie za chwilę.',
          );
          break;
        case 400:
          if (error.error?.errors) {
            const modelStateErrors: string[] = [];
            for (const key in error.error.errors) {
              if (error.error.errors[key]) {
                modelStateErrors.push(error.error.errors[key]);
              }
            }
            return throwError(() => modelStateErrors.flat());
          }
          return throwError(() => error);
        case 401:
        case 403:
          toastr.error(
            error.error?.message ??
              'Nie posiadasz uprawnień, by zobaczyć tę stronę.',
          );
          break;
        case 404:
          toastr.error('Podana strona nie została znaleziona.');
          router.navigateByUrl('/');
          break;
        case 409:
          return throwError(() => error);
        default:
          toastr.error('Błąd serwera. Spróbuj ponownie za chwilę.');
          break;
      }
      return throwError(() => error);
    }),
  );
};
