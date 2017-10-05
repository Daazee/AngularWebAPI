import { TestBed, inject } from '@angular/core/testing';

import { HttpinterceptorService } from './httpinterceptor.service';

describe('HttpinterceptorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpinterceptorService]
    });
  });

  it('should be created', inject([HttpinterceptorService], (service: HttpinterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
