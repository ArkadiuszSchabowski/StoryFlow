import { GetStoryViewDto } from "./get-story-view-dto";
import { GetWordLessonDto } from "./get-word-lesson-dto";

export interface GetStorySeasonDto {
    id: number;
    name: string;
    seasonNumber: number;
    isVisibleForUser: boolean;
    stories: GetStoryViewDto[];
    wordLessons: GetWordLessonDto[];
    maxPoints: number | null;
    pointsRequiredToUnlock: number | null;
}