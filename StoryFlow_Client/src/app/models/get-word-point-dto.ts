export interface GetWordPointDto {
    id: number;
    wordLessonId: number;
    maxPoints: number | null;
    pointsPerWord: number | null;
}