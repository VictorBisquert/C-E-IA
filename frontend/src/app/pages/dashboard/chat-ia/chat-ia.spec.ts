import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatIA } from './chat-ia';

describe('ChatIA', () => {
  let component: ChatIA;
  let fixture: ComponentFixture<ChatIA>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatIA]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatIA);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
