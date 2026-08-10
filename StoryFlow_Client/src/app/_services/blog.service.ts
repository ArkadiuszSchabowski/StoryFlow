import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GeminiResponse } from '../models/gemini-response';
import { AuthService } from './auth.service';
import { GenerateBlogPostDto } from '../models/generate-blog-post-dto';
import { AddBlogPostDto } from '../models/add-blog-post-dto';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) {}
    add(dto: AddBlogPostDto) {
      const token: string | null = this.authService.getToken();
  
      const headers = {
        Authorization: `Bearer ${token}`,
      };
  
      return this.http.post(this.apiUrl + 'blog', dto, { headers });
    }

  getVisibleBlogPosts() {
    return this.http.get(this.apiUrl + 'blog/visible');
  }

  getBySlug(slug: string) {
    return this.http.get(this.apiUrl + `blog/by-slug/${slug}`);
  }

  generate(dto: GenerateBlogPostDto) {
    const token: string | null = this.authService.getToken();

    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return this.http
      .post<GeminiResponse>(this.apiUrl + 'blog/generate', dto, {
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
}
