import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs';
import { GetStoryViewDto } from '../models/get-story-view-dto';
import { GetSentenceViewDto } from '../models/get-sentence-view-dto';
import { StoryFilter } from '../models/story-filter-dto';
import { GenerateStoryDto } from '../models/generate-story-dto';
import { GeminiResponse } from '../models/gemini-response';
import { AddStoryDto } from '../models/add-story-dto';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class StoryService {
  private apiUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private http: HttpClient,
  ) {}

  add(dto: AddStoryDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.post(this.apiUrl + 'story', dto, { headers });
  }

  generate(dto: GenerateStoryDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http
      .post<GeminiResponse>(this.apiUrl + 'story/generate', dto, {
        headers,
      })
      .pipe(
        map((response) => {
          const text = response?.candidates?.[0]?.content?.parts?.[0]?.text;

          if (!text) {
            throw new Error('Invalid Gemini response structure');
          }

          return JSON.parse(text);
        }),
      );
  }

  getAll(dto: StoryFilter | null) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    let params = new HttpParams();

    if (dto?.languageLevel)
      params = params.set('languageLevel', dto.languageLevel);
    if (dto?.category) params = params.set('category', dto.category);
    if (dto?.size) params = params.set('size', dto.size);

    return this.http.get<GetStoryViewDto[]>(this.apiUrl + 'story', {
      params,
      headers,
    });
  }

  get(id: number) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http
      .get<GetStoryViewDto>(this.apiUrl + `story/${id}`, { headers })
      .pipe(
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
            quiz: story.quiz,
            storyPoint: story.storyPoint,
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
