import { TestBed } from '@angular/core/testing';

import { ResearchLineService } from './research-line.service';

describe('ResearchLineService', () => {
  let service: ResearchLineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResearchLineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
