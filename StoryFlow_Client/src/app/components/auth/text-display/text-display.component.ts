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
  gotoQuiz() {
    this.router.navigateByUrl(`text/${this.storyId}/quiz`);
  }

  get(id: number) {
    if (this.storyId != 0) {
      this.storyService.get(id).subscribe({
        next: (response) => {
          console.log(response),
          this.story = response
        },
        error: () => this.router.navigateByUrl(`error-page`),
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
  showLanguageLevel(level: any) {
    switch (level) {
      case 0:
        return 'A1 - Beginner';
      case 1:
        return 'A2 - Elementary';
      case 2:
        return 'B1 - Intermediate';
      case 3:
        return 'B2 - Upper-Intermediate';
      case 4:
        return 'C1 - Advanced';
      case 5:
        return 'C2 - Proficiency';
      default:
        return 'Unknown';
    }
  }

  showSize(size: any) {
    switch (size) {
      case 0:
        return 'Short';
      case 1:
        return 'Medium';
      case 2:
        return 'Long';
      default:
        return 'Unknown';
    }
  }

  showCategory(category: any) {
    switch (category) {
      case 0:
        return 'Animals';
      case 1:
        return 'Health';
      case 2:
        return 'Technology';
      case 3:
        return 'Sport';
      case 4:
        return 'Art';
      case 5:
        return 'History';
      case 6:
        return 'Music';
      default:
        return 'Unknown';
    }
  }
}
