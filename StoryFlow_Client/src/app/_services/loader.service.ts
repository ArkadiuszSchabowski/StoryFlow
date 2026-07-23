import { Injectable } from '@angular/core';
import { AnimationOptions } from 'ngx-lottie';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private loadingSubject = new BehaviorSubject<boolean>(false);
  loading$ = this.loadingSubject.asObservable();

    animationOptions: AnimationOptions = {
    path: 'assets/kitty/kitty.json'
  };
  
  show() {
    this.loadingSubject.next(true);
  }

  hide() {
    this.loadingSubject.next(false);
  }
}