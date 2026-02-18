import { LanguageLevel } from '../enums/language-level';
import { StoryCategory } from '../enums/story-category';
import { StorySize } from '../enums/story-size';
import { GetSentenceViewDto } from './get-sentence-view-dto';
import { GetUserStoryDto } from './get-user-story-dto';

export class GetStoryViewDto {
  id: number = 0;
  polishStory: string = '';
  englishStory: string = '';
  polishTitle: string = '';
  englishTitle: string = '';
  polishDescription: string = '';
  englishDescription: string = '';
  storyCategory: StoryCategory | null = null;
  storySize: StorySize | null = null;
  languageLevel: LanguageLevel | null = null;
  sentences: GetSentenceViewDto[] = [];
  userStories: GetUserStoryDto[] = [];
  isDescriptionEnglish: boolean = true;
  isTitleEnglish: boolean = true;

  get visibleDescriptionMeaning(): string {
    return this.isDescriptionEnglish ? this.englishDescription : this.polishDescription;
  }

  get visibleTitleMeaning(): string {
    return this.isTitleEnglish ? this.englishTitle : this.polishTitle;
  }
}
