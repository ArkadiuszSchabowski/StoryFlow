import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StoryService } from 'src/app/_services/story.service';
import {
  CATEGORIES,
  LANGUAGELEVELS,
  SIZES,
} from 'src/app/constants/select-options';
import { AddStoryDto } from 'src/app/models/add-story-dto';
import { GenerateStoryDto } from 'src/app/models/generate-story-dto';

@Component({
    selector: 'app-story-create',
    templateUrl: './story-create.component.html',
    styleUrls: ['./story-create.component.scss'],
    standalone: false
})
export class StoryCreateComponent {
  categories = CATEGORIES;
  languageLevels = LANGUAGELEVELS;
  sizes = SIZES;

  constructor(
    private fb: FormBuilder,
    private storyService: StoryService,
    private toastr: ToastrService,
  ) {}

  generateForm: any = this.fb.group({
    languageLevel: [0],
    storyCategory: [0],
    storySize: [0],
    additionalInstructions: '',
  });

  addForm: any = this.fb.group({
    languageLevel: [''],
    storyCategory: [''],
    polishTitle: [''],
    englishTitle: [''],
    polishDescription: [''],
    englishDescription: [''],
    polishStory: [''],
    englishStory: [''],
  });

  public generate() {
    const dto: GenerateStoryDto = {
      storyCategory: this.generateForm.get('storyCategory').value,
      languageLevel: this.generateForm.get('languageLevel').value,
      storySize: this.generateForm.get('storySize').value,
      additionalInstructions: this.generateForm.get('additionalInstructions')
        .value,
    };

    this.storyService.generate(dto).subscribe({
      next: (response) => {
        this.addForm.patchValue({
          languageLevel: response.languageLevel,
          storyCategory: response.storyCategory,
          polishTitle: response.polishTitle,
          englishTitle: response.englishTitle,
          polishDescription: response.polishDescription,
          englishDescription: response.englishDescription,
          polishStory: response.polishStory,
          englishStory: response.englishStory,
        });
      },
      error: () => {},
    });
  }
  public add() {
    const dto: AddStoryDto = {
      storyCategory: this.generateForm.get('storyCategory')?.value,
      languageLevel: this.generateForm.get('languageLevel')?.value,

      polishTitle: this.addForm.get('polishTitle')?.value,
      englishTitle: this.addForm.get('englishTitle')?.value,
      polishDescription: this.addForm.get('polishDescription')?.value,
      englishDescription: this.addForm.get('englishDescription')?.value,
      polishStory: this.addForm.get('polishStory')?.value,
      englishStory: this.addForm.get('englishStory')?.value,
    };

    this.storyService.add(dto).subscribe({
      next: () => {
        this.toastr.success('Historia została dodana.');
        this.addForm.reset();
        this.generateForm.patchValue({
          additionalInstructions: '',
        });
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }
}
