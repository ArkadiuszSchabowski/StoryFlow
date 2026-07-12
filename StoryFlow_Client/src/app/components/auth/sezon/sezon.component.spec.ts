import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SezonComponent } from './sezon.component';

describe('SezonComponent', () => {
  let component: SezonComponent;
  let fixture: ComponentFixture<SezonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SezonComponent]
    });
    fixture = TestBed.createComponent(SezonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
