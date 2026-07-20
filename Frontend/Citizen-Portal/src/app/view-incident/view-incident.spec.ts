import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewIncident } from './view-incident';

describe('ViewIncident', () => {
  let component: ViewIncident;
  let fixture: ComponentFixture<ViewIncident>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewIncident],
    }).compileComponents();

    fixture = TestBed.createComponent(ViewIncident);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
