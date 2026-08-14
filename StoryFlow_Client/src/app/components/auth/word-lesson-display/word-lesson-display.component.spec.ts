import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WordLessonDisplayComponent } from './word-lesson-display.component';

describe('WordLessonDisplayComponent', () => {
  let component: WordLessonDisplayComponent;
  let fixture: ComponentFixture<WordLessonDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WordLessonDisplayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WordLessonDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
