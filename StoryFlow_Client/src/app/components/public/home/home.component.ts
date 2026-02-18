import { Component } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';
import { GetSentenceViewDto } from 'src/app/models/get-sentence-view-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  story: GetStoryViewDto = new GetStoryViewDto();

  constructor(private storyService: StoryService) {
    this.get(1);
  }

  get(id: number) {
    this.storyService.get(id).subscribe({
      next: (response) => {
        this.story = response;
      },
      error: (error) => console.log(error),
    });
  }

  changeDescriptionLanguage(story: GetStoryViewDto) {
    story.isDescriptionEnglish = !story.isDescriptionEnglish;
  }
  changeSentenceLanguage(sentence: GetSentenceViewDto) {
    sentence.isSentenceEnglish = !sentence.isSentenceEnglish;
  }
  changeTitleLanguage(story: GetStoryViewDto) {
    story.isTitleEnglish = !story.isTitleEnglish;
  }
}
