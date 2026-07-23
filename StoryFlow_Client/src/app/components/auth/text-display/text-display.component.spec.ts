import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextDisplayComponent } from './text-display.component';
import { ActivatedRoute } from '@angular/router';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { of } from 'rxjs';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

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
    imports: [],
    providers: [{ provide: ActivatedRoute, useClass: MockActivatedRoute }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
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
