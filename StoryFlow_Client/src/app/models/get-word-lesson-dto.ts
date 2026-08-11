import { GetWordDto } from "./get-word-dto";

export interface GetWordLessonDto {
    id: number;
    orderInSeason: number | null;
    storySeasonId: number | null;
    polishTitlte: string | null;
    words: GetWordDto[];
}