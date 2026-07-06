import { Injectable } from '@angular/core';
import { LoginDto } from '../models/login-dto';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { TokenDto } from '../models/token-dto';
import { AuthService } from './auth.service';
import { tap } from 'rxjs';
import { RegisterUserDto } from '../models/register-user-dto';
import { GetUserDto } from '../models/get-user-dto';
import { LoaderService } from './loader.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private loaderService: LoaderService
  ) {}

  getProfile() {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.get<GetUserDto>(this.apiUrl + 'user/profile', {headers});
  }

login(dto: LoginDto) {
  this.loaderService.show();

  const start = Date.now();
  const minTime = 3000;

  return this.http.post<TokenDto>(this.apiUrl + 'user/login', dto).pipe(
    tap((response) => {
      if (!response.token) return;

      const elapsed = Date.now() - start;
      const remaining = Math.max(0, minTime - elapsed);

      setTimeout(() => {
        this.loaderService.hide();
        this.authService.setUser(response.token);
      }, remaining);
    })
  );
}

  register(dto: RegisterUserDto) {
    return this.http.post(this.apiUrl + 'user/register', dto);
  }
}
