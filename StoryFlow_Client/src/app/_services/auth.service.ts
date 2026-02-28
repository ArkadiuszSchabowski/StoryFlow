import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSource = new BehaviorSubject<string | null>(null);
  currentUserSource$ = this.currentUserSource.asObservable();

  constructor() { }
  
  setUser(token: string){
    localStorage.setItem('token', token)
    this.currentUserSource.next(token);
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }
}
