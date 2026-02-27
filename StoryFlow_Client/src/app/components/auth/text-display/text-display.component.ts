import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/_services/story.service';
import { GetSentenceViewDto } from 'src/app/models/get-sentence-view-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-text-display',
  templateUrl: './text-display.component.html',
  styleUrls: ['./text-display.component.scss'],
})
export class TextDisplayComponent implements OnInit {
  storyId: number = 0;
  story: GetStoryViewDto | null = null;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private storyService: StoryService,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.storyId = id;
      this.get(id);
    });
  }

  get(id: number) {
    if (this.storyId != 0) {
      this.storyService.get(id).subscribe({
        next: (response) => {
          ((this.story = response), console.log(response));
        },
        error: (error) => this.router.navigateByUrl((`error-page`)),
      });
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
