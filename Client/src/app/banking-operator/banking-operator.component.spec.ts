import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankingOperatorComponent } from './banking-operator.component';

describe('BankingOperatorComponent', () => {
  let component: BankingOperatorComponent;
  let fixture: ComponentFixture<BankingOperatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankingOperatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BankingOperatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
