import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextSelectionComponent } from './text-selection.component';

describe('TextSelectionComponent', () => {
  let component: TextSelectionComponent;
  let fixture: ComponentFixture<TextSelectionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TextSelectionComponent]
    });
    fixture = TestBed.createComponent(TextSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
