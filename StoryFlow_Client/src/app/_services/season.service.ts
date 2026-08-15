import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { GetStoryViewDto } from '../models/get-story-view-dto';
import { AuthService } from './auth.service';
import { GetStorySeasonDto } from '../models/get-story-season-dto';

@Injectable({
  providedIn: 'root',
})
export class SeasonService {
  private apiUrl = environment.apiUrl;

  constructor(
    private authService: AuthService,
    private http: HttpClient,
  ) {}

  getBySeason(id: number) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http.get<GetStorySeasonDto>(
      this.apiUrl + `season/${id}`,
      { headers },
    );
  }
}
