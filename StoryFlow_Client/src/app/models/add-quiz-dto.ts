import { AddQuestionDto } from "./add-question-dto";

export interface AddQuizDto {
    storyId: number;
    questions: AddQuestionDto[];
}