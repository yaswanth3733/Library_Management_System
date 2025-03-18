import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestBookComponent } from './request-book.component';

describe('RequestBookComponent', () => {
  let component: RequestBookComponent;
  let fixture: ComponentFixture<RequestBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequestBookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
