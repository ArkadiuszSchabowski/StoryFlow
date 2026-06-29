import { GetStoryDto } from "./get-story-dto";

export interface GetStoryPoint {
    id: number;
    storyId: number;
    story: GetStoryDto | null;
    maxPoints: number | null;
    pointsPerAnswer: number | null;
    bonusPointsForStoryLength: number | null;
}