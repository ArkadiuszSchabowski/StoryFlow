import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StoryService } from 'src/app/_services/story.service';
import { TextService } from 'src/app/_services/text.service';

@Component({
  selector: 'app-text-selection',
  templateUrl: './text-selection.component.html',
  styleUrls: ['./text-selection.component.scss'],
})
export class TextSelectionComponent {

  stories: any;
  selected = 'option2';

  constructor(
    private storyService: StoryService,
    private textService: TextService,
    private router: Router
  ) {
    this.getAll();
  }

  getAll() {
    this.storyService.getAll().subscribe({
      next: (response) => {
        ((this.stories = response), console.log(this.stories));
      },

      error: (error) => console.log(error),
    });
  }

  get(id: number) {
    this.textService.storyIdSubject.next(id);
    this.router.navigateByUrl('text');
  }
}
