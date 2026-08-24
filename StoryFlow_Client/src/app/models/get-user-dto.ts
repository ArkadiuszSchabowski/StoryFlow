import { GetUserPreferencesDto } from "./get-user-preferences-dto";

export interface GetUserDto {
    id: number;
    email: string | null;
    firstName: string | null;
    nick: string | null;
    gender: number | null;
    dateOfBirth: string | null;
    stars: number | null;
    tickets: number;
    hobbies: [];
    userPreferences: GetUserPreferencesDto;
}