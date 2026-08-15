import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { GetWordLessonDto } from '../models/get-word-lesson-dto';
import { AddWordResultDto } from '../models/add-word-lesson-dto';

@Injectable({
  providedIn: 'root',
})
export class WordLessonService {
  private apiUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private http: HttpClient,
  ) {}

  get(id: number) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.get<GetWordLessonDto>(this.apiUrl + `word/lesson/${id}`, {
      headers,
    });
  }

  save(lessonId: number, dto: AddWordResultDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.post(
      this.apiUrl + `word/lesson/save/${lessonId}`,
      dto,
      {
        headers,
      },
    );
  }
}
