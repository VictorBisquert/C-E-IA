import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Basculas } from './basculas';

describe('Basculas', () => {
  let component: Basculas;
  let fixture: ComponentFixture<Basculas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Basculas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Basculas);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
