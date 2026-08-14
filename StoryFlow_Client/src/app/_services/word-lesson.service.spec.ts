import { TestBed } from '@angular/core/testing';

import { WordLessonService } from './word-lesson.service';

describe('WordLessonService', () => {
  let service: WordLessonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WordLessonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
