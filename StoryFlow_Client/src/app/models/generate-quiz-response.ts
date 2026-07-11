import { GenerateQuestionDto } from "./generate-question-dto";

export interface GenerateQuizResponseDto {
  questions: GenerateQuestionDto[];
}