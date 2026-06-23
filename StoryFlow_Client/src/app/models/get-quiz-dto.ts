import { GetQuestionDto } from "./get-question-dto";

export interface GetQuizDto {
    id: number;
    storyId: number;
    questions: GetQuestionDto[];
}