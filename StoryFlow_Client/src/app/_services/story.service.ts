import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { GetStoryDto } from '../models/get-story-dto';

@Injectable({
  providedIn: 'root'
})
export class StoryService {

  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
    
   }

   get(id: number){
    return this.http.get(this.apiUrl + `story/${id}`)
   }


}
