import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { map } from 'rxjs';
import { GetStoryViewDto } from '../models/get-story-view-dto';
import { GetSentenceViewDto } from '../models/get-sentence-view-dto';
import { StoryFilter } from '../models/story-filter-dto';

@Injectable({
  providedIn: 'root',
})
export class StoryService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAll(dto: StoryFilter | null) {
    let params = new HttpParams();

    if (dto?.languageLevel)
      params = params.set('languageLevel', dto.languageLevel);
    if (dto?.category) params = params.set('category', dto.category);
    if (dto?.size) params = params.set('size', dto.size);

    return this.http.get<GetStoryViewDto[]>(this.apiUrl + 'story', { params });
  }

  get(id: number) {
    return this.http.get<GetStoryViewDto>(this.apiUrl + `story/${id}`).pipe(
      map((story): GetStoryViewDto => {
        return Object.assign(new GetStoryViewDto(), {
          id: story.id,
          polishStory: story.polishStory,
          englishStory: story.englishStory,
          polishTitle: story.polishTitle,
          englishTitle: story.englishTitle,
          polishDescription: story.polishDescription,
          englishDescription: story.englishDescription,
          storyCategory: story.storyCategory,
          storySize: story.storySize,
          languageLevel: story.languageLevel,
          userStories: story.userStories,
          sentences: story.sentences.map((sentence): GetSentenceViewDto => {
            return Object.assign(new GetSentenceViewDto(), {
              id: sentence.id,
              storyId: sentence.storyId,
              order: sentence.order,
              polishMeaning: sentence.polishMeaning,
              englishMeaning: sentence.englishMeaning,
              userSentences: sentence.userSentences,
            });
          }),
        });
      }),
    );
  }
}
