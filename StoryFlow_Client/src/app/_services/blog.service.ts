import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get(this.apiUrl + 'blog');
  }

  getBySlug(slug: string){
    return this.http.get(this.apiUrl + `blog/by-slug/${slug}`)
  }
}
