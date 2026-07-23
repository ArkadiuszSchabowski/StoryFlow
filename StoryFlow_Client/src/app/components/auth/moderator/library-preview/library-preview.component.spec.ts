import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LibraryPreviewComponent } from './library-preview.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';
import { MaterialModule } from 'src/app/modules/material/material.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('LibraryPreviewComponent',() => {
  let component: LibraryPreviewComponent;
  let fixture: ComponentFixture<LibraryPreviewComponent>;
  let toastrService: ToastrService;

  class MockToastrService {}

  beforeEach(() => {
    TestBed.configureTestingModule({
    declarations: [LibraryPreviewComponent],
    imports: [BrowserAnimationsModule, MaterialModule, ReactiveFormsModule],
    providers: [{ provide: ToastrService, useClass: MockToastrService }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
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