import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StoryService } from 'src/app/_services/story.service';
import { AddStoryDto } from 'src/app/models/add-story-dto';
import { GenerateStoryDto } from 'src/app/models/generate-story-dto';

@Component({
  selector: 'app-story-create',
  templateUrl: './story-create.component.html',
  styleUrls: ['./story-create.component.scss'],
})
export class StoryCreateComponent {
  constructor(
    private fb: FormBuilder,
    private storyService: StoryService,
    private toastr: ToastrService,
  ) {}

  generateForm: any = this.fb.group({
    languageLevel: [0],
    storyCategory: [0],
    storySize: [0],
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

  categories = [
    { value: 0, viewValue: 'Animals' },
    { value: 1, viewValue: 'Health' },
    { value: 2, viewValue: 'Technology' },
    { value: 3, viewValue: 'Sport' },
    { value: 4, viewValue: 'Art' },
    { value: 5, viewValue: 'History' },
    { value: 6, viewValue: 'Music' },
  ];

  languageLevels = [
    { value: 0, viewValue: 'A1 - Beginner' },
    { value: 1, viewValue: 'A2 - Elementary' },
    { value: 2, viewValue: 'B1 - Intermediate' },
    { value: 3, viewValue: 'B2 - Upper-Intermediate' },
    { value: 4, viewValue: 'C1 - Advanced' },
    { value: 5, viewValue: 'C2 - Proficiency' },
  ];

  sizes = [
    { value: 0, viewValue: 'Short' },
    { value: 1, viewValue: 'Medium' },
    { value: 2, viewValue: 'Long' },
  ];

  public generate() {
    const dto: GenerateStoryDto = {
      storyCategory: this.generateForm.get('storyCategory').value,
      languageLevel: this.generateForm.get('languageLevel').value,
      storySize: this.generateForm.get('storySize').value,
    };

    console.log(dto);

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
      next: (response) => {
        console.log(response);
        this.addForm.reset();
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }
}
