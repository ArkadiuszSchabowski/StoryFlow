import { LanguageLevel } from "../enums/language-level";
import { StoryCategory } from "../enums/story-category";
import { StorySize } from "../enums/story-size";
import { GetSentenceDto } from "./get-sentence-dto";
import { GetUserStoryDto } from "./get-user-story-dto";


export class GetStoryViewDto {
    id: number = 0;
    polishStory: string | null = null;
    englishStory: string | null = null;
    polishTitle: string | null = null;
    englishTitle: string | null = null;
    polishDescription: string | null = null;
    englishDescription: string | null = null;
    storyCategory: StoryCategory | null = null;
    storySize: StorySize | null = null;
    languageLevel: LanguageLevel | null = null;
    sentences: GetSentenceDto[] =[];
    userStories: GetUserStoryDto[] = [];
}