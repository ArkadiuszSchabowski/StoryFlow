import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';
import { TextService } from 'src/app/_services/text.service';
import { GetSentenceViewDto } from 'src/app/models/get-sentence-view-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';

@Component({
  selector: 'app-text-display',
  templateUrl: './text-display.component.html',
  styleUrls: ['./text-display.component.scss']
})
export class TextDisplayComponent implements OnInit {

  storyId: number = 0;
  story: GetStoryViewDto = new GetStoryViewDto();

  constructor(private storyService: StoryService, private textService: TextService){
  }
  ngOnInit(): void {
    this.getTextId();
  }

  getTextId(){
    this.textService.storyId$.subscribe({
      next: response => {
        this.storyId = response,
        this.get(this.storyId)
      },
      error: error => console.log(error)
    })
  }

  get(id: number){
    if(this.storyId != 0){
      this.storyService.get(id).subscribe({
        next: response => {
          this.story = response,
          console.log(response)
        },
        error: error => console.log(error)
      })
    }
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
