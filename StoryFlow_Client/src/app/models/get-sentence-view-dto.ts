import { GetUserSentenceDto } from "./get-user-sentence-dto";

export class GetSentenceViewDto {
    id: number = 0;
    storyId: number = 0;
    order: number | null = null;
    polishMeaning: string = '';
    englishMeaning: string = '';
    userSentences: GetUserSentenceDto[] = [];
    isEnglishVisibleMeaning: boolean = true;
    
    get visibleMeaning(): string {
        return this.isEnglishVisibleMeaning ? this.englishMeaning : this.polishMeaning;
    }
}