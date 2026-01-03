import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalParametrosGenerales } from './modal-parametros-generales';

describe('ModalParametrosGenerales', () => {
  let component: ModalParametrosGenerales;
  let fixture: ComponentFixture<ModalParametrosGenerales>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalParametrosGenerales]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalParametrosGenerales);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
