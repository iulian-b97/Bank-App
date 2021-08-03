import { TestBed } from '@angular/core/testing';

import { BankingOperatorService } from './banking-operator.service';

describe('BankingOperatorService', () => {
  let service: BankingOperatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BankingOperatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
