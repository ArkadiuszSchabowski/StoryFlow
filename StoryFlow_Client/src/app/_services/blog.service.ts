import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getVisibleBlogPosts() {
    return this.http.get(this.apiUrl + 'blog/visible');
  }

  getBySlug(slug: string){
    return this.http.get(this.apiUrl + `blog/by-slug/${slug}`)
  }
}
