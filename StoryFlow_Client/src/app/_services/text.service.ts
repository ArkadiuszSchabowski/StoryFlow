import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TextService {
  constructor() {}

  storyIdSubject = new BehaviorSubject<number>(0);
  storyId$ = this.storyIdSubject.asObservable();
}
