import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMenuComponent } from './report-menu.component';

describe('ReportMenuComponent', () => {
  let component: ReportMenuComponent;
  let fixture: ComponentFixture<ReportMenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportMenuComponent]
    });
    fixture = TestBed.createComponent(ReportMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
