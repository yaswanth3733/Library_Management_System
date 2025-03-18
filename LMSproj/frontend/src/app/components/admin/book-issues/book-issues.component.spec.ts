import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookIssuesComponent } from './book-issues.component';

describe('BookIssuesComponent', () => {
  let component: BookIssuesComponent;
  let fixture: ComponentFixture<BookIssuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookIssuesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
