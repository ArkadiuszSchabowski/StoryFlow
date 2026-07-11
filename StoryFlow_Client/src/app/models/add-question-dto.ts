import { AddAnswerDto } from "./add-answer-dto";

export interface AddQuestionDto {
    questionText: string;
    answers: AddAnswerDto[];
}