import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetQuizComponent } from './get-quiz.component';

describe('GetQuizComponent', () => {
  let component: GetQuizComponent;
  let fixture: ComponentFixture<GetQuizComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GetQuizComponent]
    });
    fixture = TestBed.createComponent(GetQuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
