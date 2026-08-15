export interface GetWordDto {
    id: number;
    polishWord: string | null;
    englishWord: string | null;
    imageUrl: string | null;
    hintSentence: string | null;
}