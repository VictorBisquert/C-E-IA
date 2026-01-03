import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScaleData } from './scale-data';

describe('ScaleData', () => {
  let component: ScaleData;
  let fixture: ComponentFixture<ScaleData>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScaleData]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScaleData);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
