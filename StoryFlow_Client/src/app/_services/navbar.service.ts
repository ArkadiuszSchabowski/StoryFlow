import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NavbarService {
  private navbarPublicButtonSubject = new BehaviorSubject<boolean>(true);
  navbarPublicButton$ = this.navbarPublicButtonSubject.asObservable();

  constructor() {}

  unblockButtons() {
    this.navbarPublicButtonSubject.next(true);
  }

  blockButtons() {
    this.navbarPublicButtonSubject.next(false);
  }
}
