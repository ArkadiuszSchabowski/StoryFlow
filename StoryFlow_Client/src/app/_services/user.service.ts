import { Injectable } from '@angular/core';
import { LoginDto } from '../models/login-dto';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { TokenDto } from '../models/token-dto';
import { AuthService } from './auth.service';
import { tap } from 'rxjs';
import { RegisterUserDto } from '../models/register-user-dto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) {}

  

  login(dto: LoginDto) {

    return this.http.post<TokenDto>(this.apiUrl + 'user/login', dto).pipe(tap((response) => {
      if(response.token){
        this.authService.setUser(response.token);
      }
    }))
  }

  register(dto: RegisterUserDto){
    return this.http.post(this.apiUrl + 'user/register', dto)
  }
}
