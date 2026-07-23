import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { StoryService } from './story.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('StoryService', () => {
  let service: StoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    service = TestBed.inject(StoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
