import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { AuthService } from './auth.service';
import { QuizSubmissionDto } from '../models/quiz-submission-dto';

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

    return this.http.post(this.apiUrl + 'quiz/check', dto, { headers });
  }
}
