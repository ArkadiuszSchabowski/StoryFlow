import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { QuizSubmissionDto } from '../models/quiz-submission-dto';
import { QuizResult } from '../models/quiz-result';

@Injectable({
  providedIn: 'root',
})
export class QuizService {
  private apiUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private http: HttpClient,
  ) {}

  sendAnswers(dto: QuizSubmissionDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.post<QuizResult | null>(this.apiUrl + 'quiz/submit', dto, { headers });
  }
}
