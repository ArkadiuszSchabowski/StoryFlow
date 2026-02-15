import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class StoryService {

  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
    
   }

   GetAll(){
    return this.http.get(this.apiUrl + 'story')
   }

   Get(id: number){
    return this.http.get(this.apiUrl + `story/${id}`)
   }
}
