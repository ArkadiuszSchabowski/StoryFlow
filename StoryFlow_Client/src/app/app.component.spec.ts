import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { ToastrService } from 'ngx-toastr';
import { MaterialModule } from './modules/material/material.module';

describe('AppComponent', () => {
  class MockToastrService {}

  beforeEach(() =>
    TestBed.configureTestingModule({
      declarations: [AppComponent, NavbarComponent],
      imports: [RouterTestingModule, MaterialModule],
      providers: [{ provide: ToastrService, useClass: MockToastrService }],
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
