import { Component } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  story: any = null;

  constructor(private storyService: StoryService) {
    this.get(1);
  }

  get(id: number) {
    this.storyService.get(id).subscribe({
      next: (response) => {
        this.story = response;
        console.log(this.story);
      },
      error: (error) => console.log(error),
    });
  }

  changeSentenceLanguage(sentence: any) {
    sentence.isEnglishVisibleMeaning = !sentence.isEnglishVisibleMeaning;
  }
}
