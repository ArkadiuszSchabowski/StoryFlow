import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextDisplayComponent } from './text-display.component';
import { ActivatedRoute } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';

describe('TextDisplayComponent', () => {
  let component: TextDisplayComponent;
  let fixture: ComponentFixture<TextDisplayComponent>;
  let activatedRoute: ActivatedRoute;

class MockActivatedRoute {
  paramMap = of({
    get: (key: string) => '1',
  });
}

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TextDisplayComponent],
      imports: [HttpClientTestingModule],
      providers: [{ provide: ActivatedRoute, useClass: MockActivatedRoute }],
    });
    fixture = TestBed.createComponent(TextDisplayComponent);
    component = fixture.componentInstance;
    activatedRoute = TestBed.inject(ActivatedRoute);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
