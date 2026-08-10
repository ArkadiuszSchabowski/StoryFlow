import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { StoryCreateComponent } from './story-create.component';
import { ToastrService } from 'ngx-toastr';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('StoryCreateComponent', () => {
  let component: StoryCreateComponent;
  let fixture: ComponentFixture<StoryCreateComponent>;
  let toastrService: ToastrService;

  class MockToastrService {}

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [StoryCreateComponent, BrowserAnimationsModule, ReactiveFormsModule],
    providers: [{ provide: ToastrService, useClass: MockToastrService }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    fixture = TestBed.createComponent(StoryCreateComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
