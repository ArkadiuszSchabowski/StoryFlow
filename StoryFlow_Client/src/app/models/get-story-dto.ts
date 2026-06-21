import { LanguageLevel } from "../enums/language-level";
import { StoryCategory } from "../enums/story-category";
import { StorySize } from "../enums/story-size";
import { GetSentenceDto } from "./get-sentence-dto";
import { GetUserStoryDto } from "./get-user-story-dto";


export interface GetStoryDto {
    id: number;
    polishStory: string | null;
    englishStory: string | null;
    polishTitle: string | null;
    englishTitle: string | null;
    polishDescription: string | null;
    englishDescription: string | null;
    storyCategory: StoryCategory | null;
    storySize: StorySize | null;
    maxPoints: number;
    languageLevel: LanguageLevel | null;
    sentences: GetSentenceDto[];
    userStories: GetUserStoryDto[];
}