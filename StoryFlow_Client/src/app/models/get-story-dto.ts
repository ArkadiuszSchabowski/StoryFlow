import { LanguageLevel } from "../enums/language-level";
import { StoryCategory } from "../enums/story-category";
import { StorySize } from "../enums/story-size";
import { GetQuizDto } from "./get-quiz-dto";
import { GetSentenceDto } from "./get-sentence-dto";
import { GetUserStoryDto } from "./get-user-story-dto";
import { GetStoryPoint } from "./get-story-points";


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
    storyPointId: number;
    storyPoint: GetStoryPoint | null;
    languageLevel: LanguageLevel | null;
    sentences: GetSentenceDto[];
    userStories: GetUserStoryDto[];
    quiz: GetQuizDto;
    numberOfQuestions : number;
}