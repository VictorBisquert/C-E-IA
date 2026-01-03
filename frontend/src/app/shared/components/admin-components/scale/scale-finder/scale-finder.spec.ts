import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScaleFinder } from './scale-finder';

describe('ScaleFinder', () => {
  let component: ScaleFinder;
  let fixture: ComponentFixture<ScaleFinder>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScaleFinder]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScaleFinder);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
