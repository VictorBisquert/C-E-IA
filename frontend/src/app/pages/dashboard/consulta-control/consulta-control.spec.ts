import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaControl } from './consulta-control';

describe('ConsultaControl', () => {
  let component: ConsultaControl;
  let fixture: ComponentFixture<ConsultaControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultaControl]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultaControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
