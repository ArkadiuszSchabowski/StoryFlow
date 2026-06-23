export interface GetAnswerDto {
    id: number;
    questionId: number;
    key: string;
    text: string;
    isCorrect: boolean;
}