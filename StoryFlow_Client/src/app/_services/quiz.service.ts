import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { QuizSubmissionDto } from '../models/quiz-submission-dto';
import { QuizResult } from '../models/quiz-result';
import { GeminiResponse } from '../models/gemini-response';
import { map } from 'rxjs';
import { GenerateQuizDto } from '../models/generate-quiz-dto';
import { AddQuizDto } from '../models/add-quiz-dto';

@Injectable({
  providedIn: 'root',
})
export class QuizService {
  private apiUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private http: HttpClient,
  ) {}

  add(dto: AddQuizDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.post(this.apiUrl + 'quiz', dto, {headers});
  }

  generate(dto: GenerateQuizDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http
      .post<GeminiResponse>(this.apiUrl + 'quiz/generate', dto, {
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

  sendAnswers(dto: QuizSubmissionDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.post<QuizResult | null>(this.apiUrl + 'quiz/submit', dto, {
      headers,
    });
  }
}
