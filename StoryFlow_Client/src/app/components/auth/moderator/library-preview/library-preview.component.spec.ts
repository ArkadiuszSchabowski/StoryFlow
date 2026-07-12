import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LibraryPreviewComponent } from './library-preview.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';
import { MaterialModule } from 'src/app/modules/material/material.module';

describe('LibraryPreviewComponent',() => {
  let component: LibraryPreviewComponent;
  let fixture: ComponentFixture<LibraryPreviewComponent>;
  let toastrService: ToastrService;

  class MockToastrService {}

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LibraryPreviewComponent],
      imports: [BrowserAnimationsModule, HttpClientTestingModule, MaterialModule, ReactiveFormsModule],
      providers: [{ provide: ToastrService, useClass: MockToastrService }],
    });
    fixture = TestBed.createComponent(LibraryPreviewComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});