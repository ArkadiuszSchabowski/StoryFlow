import { AnswerDto } from "./answer-dto";

export interface QuizSubmissionDto {
    storyId: number;
    answers: AnswerDto[];
}