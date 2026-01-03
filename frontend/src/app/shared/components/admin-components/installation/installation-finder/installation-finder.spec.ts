import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstallationFinder } from './installation-finder';

describe('InstallationFinder', () => {
  let component: InstallationFinder;
  let fixture: ComponentFixture<InstallationFinder>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstallationFinder]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstallationFinder);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
