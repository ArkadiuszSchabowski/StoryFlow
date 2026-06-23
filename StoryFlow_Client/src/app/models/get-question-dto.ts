import { GetAnswerDto } from "./get-answer-dto";

export interface GetQuestionDto {
    id: number;
    quizId: number;
    questionText: string;
    answers: GetAnswerDto[];
}