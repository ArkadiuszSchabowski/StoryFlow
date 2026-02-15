import { Component } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(private storyService: StoryService) {
    this.get(1);
  }
  get(id: number){
    this.storyService.Get(id).subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
  })
  }
}
