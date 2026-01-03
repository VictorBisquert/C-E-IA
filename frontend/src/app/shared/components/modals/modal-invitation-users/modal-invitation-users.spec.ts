import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalInvitationUsers } from './modal-invitation-users';

describe('ModalInvitationUsers', () => {
  let component: ModalInvitationUsers;
  let fixture: ComponentFixture<ModalInvitationUsers>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalInvitationUsers]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalInvitationUsers);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
