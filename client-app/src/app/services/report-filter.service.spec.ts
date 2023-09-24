import { TestBed } from '@angular/core/testing';

import { ReportFilterService } from './report-filter.service';

describe('ReportFilterService', () => {
  let service: ReportFilterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReportFilterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
