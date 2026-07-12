import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SezonSelectionComponent } from './sezon-selection.component';

describe('SezonSelectionComponent', () => {
  let component: SezonSelectionComponent;
  let fixture: ComponentFixture<SezonSelectionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SezonSelectionComponent]
    });
    fixture = TestBed.createComponent(SezonSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
