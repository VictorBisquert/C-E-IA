import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalConexionVisores } from './modal-conexion-visores';

describe('ModalConexionVisores', () => {
  let component: ModalConexionVisores;
  let fixture: ComponentFixture<ModalConexionVisores>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalConexionVisores]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalConexionVisores);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
