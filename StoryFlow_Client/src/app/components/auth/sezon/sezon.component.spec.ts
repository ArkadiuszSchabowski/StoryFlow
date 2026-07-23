import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SezonComponent } from './sezon.component';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ToastrService } from 'ngx-toastr';

describe('SezonComponent', () => {
  let component: SezonComponent;
  let fixture: ComponentFixture<SezonComponent>;
  let activatedRoute: ActivatedRoute;
  class MockToastrService {}

  class MockActivatedRoute {
    paramMap = of({
      get: (key: string) => '1',
    });
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SezonComponent],
      providers: [
        { provide: ActivatedRoute, useClass: MockActivatedRoute },
        { provide: ToastrService, useClass: MockToastrService },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting(),
      ],
    });
    fixture = TestBed.createComponent(SezonComponent);
    component = fixture.componentInstance;
    activatedRoute = TestBed.inject(ActivatedRoute);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
