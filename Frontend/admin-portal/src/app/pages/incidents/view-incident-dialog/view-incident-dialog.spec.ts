import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewIncidentDialog } from './view-incident-dialog';

describe('ViewIncidentDialog', () => {
  let component: ViewIncidentDialog;
  let fixture: ComponentFixture<ViewIncidentDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewIncidentDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(ViewIncidentDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
