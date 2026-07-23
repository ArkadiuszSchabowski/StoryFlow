import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { ToastrService } from 'ngx-toastr';
import { MaterialModule } from './modules/material/material.module';
import { provideRouter } from '@angular/router';

describe('AppComponent', () => {
  class MockToastrService {}

  beforeEach(() =>
    TestBed.configureTestingModule({
      declarations: [AppComponent, NavbarComponent],
      imports: [MaterialModule],
      providers: [
        { provide: ToastrService, useClass: MockToastrService },
        provideRouter([]),
      ],
    }),
  );

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'StoryFlow_Client'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('StoryFlow_Client');
  });
});
