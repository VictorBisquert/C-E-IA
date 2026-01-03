import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Pesadas } from './pesadas';

describe('Pesadas', () => {
  let component: Pesadas;
  let fixture: ComponentFixture<Pesadas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Pesadas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Pesadas);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
