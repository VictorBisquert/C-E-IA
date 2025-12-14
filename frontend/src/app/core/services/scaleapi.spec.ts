import { TestBed } from '@angular/core/testing';

import { Scaleapi } from './scaleapi';

describe('Scaleapi', () => {
  let service: Scaleapi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Scaleapi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
