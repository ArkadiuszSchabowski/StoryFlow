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

        this.story.sentences.forEach((s: any) => {
          s.isEnglish = true;
          s.translated = s.englishMeaning;
        });
      },
      error: (error) => console.log(error),
    });
  }

  translateSentence(sentence: any) {
    sentence.isEnglish = !sentence.isEnglish;

    sentence.translated = sentence.isEnglish
      ? sentence.englishMeaning
      : sentence.polishMeaning;
  }
}
