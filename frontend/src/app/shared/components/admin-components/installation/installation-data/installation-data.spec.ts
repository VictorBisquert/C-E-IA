import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstallationData } from './installation-data';

describe('InstallationData', () => {
  let component: InstallationData;
  let fixture: ComponentFixture<InstallationData>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstallationData]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstallationData);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
