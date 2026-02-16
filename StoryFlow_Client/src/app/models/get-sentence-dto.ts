import { GetUserSentenceDto } from "./get-user-sentence-dto";

export interface GetSentenceDto {
    id: number;
    storyId: number;
    order: number | null;
    polishMeaning: string;
    englishMeaning: string;
    userSentences: GetUserSentenceDto[];
}