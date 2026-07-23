import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizCreateComponent } from './quiz-create.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ToastrService } from 'ngx-toastr';

describe('QuizCreateComponent', () => {
  let component: QuizCreateComponent;
  let fixture: ComponentFixture<QuizCreateComponent>;
  class MockToastrService {}

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [QuizCreateComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: ToastrService, useClass: MockToastrService },
      ],
    });
    fixture = TestBed.createComponent(QuizCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
