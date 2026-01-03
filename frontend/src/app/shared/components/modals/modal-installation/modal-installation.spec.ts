import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalInstallation } from './modal-installation';

describe('ModalInstallation', () => {
  let component: ModalInstallation;
  let fixture: ComponentFixture<ModalInstallation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalInstallation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalInstallation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
