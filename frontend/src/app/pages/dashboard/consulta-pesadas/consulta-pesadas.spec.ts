import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaPesadas } from './consulta-pesadas';

describe('ConsultaPesadas', () => {
  let component: ConsultaPesadas;
  let fixture: ComponentFixture<ConsultaPesadas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultaPesadas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultaPesadas);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
