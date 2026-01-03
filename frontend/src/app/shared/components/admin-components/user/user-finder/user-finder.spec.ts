import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserFinder } from './user-finder';

describe('UserFinder', () => {
  let component: UserFinder;
  let fixture: ComponentFixture<UserFinder>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserFinder]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserFinder);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
