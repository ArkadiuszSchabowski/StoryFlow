import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextDisplayComponent } from './text-display.component';

describe('TextDisplayComponent', () => {
  let component: TextDisplayComponent;
  let fixture: ComponentFixture<TextDisplayComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TextDisplayComponent]
    });
    fixture = TestBed.createComponent(TextDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
