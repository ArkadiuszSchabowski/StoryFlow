import { GetUserSentenceDto } from "./get-user-sentence-dto";

export class GetSentenceViewDto {
    id: number = 0;
    storyId: number = 0;
    order: number | null = null;
    polishMeaning: string = '';
    englishMeaning: string = '';
    userSentences: GetUserSentenceDto[] = [];
    isSentenceEnglish: boolean = true;
    
    get visibleSentenceMeaning(): string {
        return this.isSentenceEnglish ? this.englishMeaning : this.polishMeaning;
    }
}