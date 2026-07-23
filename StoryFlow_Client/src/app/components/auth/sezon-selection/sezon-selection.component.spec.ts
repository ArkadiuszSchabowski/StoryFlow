import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SezonSelectionComponent } from './sezon-selection.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ToastrService } from 'ngx-toastr';

describe('SezonSelectionComponent', () => {
  let component: SezonSelectionComponent;
  let fixture: ComponentFixture<SezonSelectionComponent>;
    class MockToastrService {}

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SezonSelectionComponent],
      providers: [provideHttpClient(), provideHttpClientTesting(), { provide: ToastrService, useClass: MockToastrService }],
    });
    fixture = TestBed.createComponent(SezonSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
