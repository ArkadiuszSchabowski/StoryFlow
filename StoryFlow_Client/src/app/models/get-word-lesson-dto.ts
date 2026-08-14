import { GetUserWordLessonDto } from "./get-user-word-lesson-dto";
import { GetWordDto } from "./get-word-dto";
import { GetWordPointDto } from "./get-word-point-dto";

export interface GetWordLessonDto {
    id: number;
    orderInSeason: number | null;
    storySeasonId: number | null;
    polishTitlte: string | null;
    image: string | null;
    words: GetWordDto[];
    userWordLessons: GetUserWordLessonDto[];
    wordPoint: GetWordPointDto | null;
}