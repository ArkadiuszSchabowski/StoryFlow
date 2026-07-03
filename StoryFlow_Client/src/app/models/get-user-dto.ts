export interface GetUserDto {
    id: number;
    email: string | null;
    firstName: string | null;
    lastName: string | null;
    gender: number | null;
    dateOfBirth: string | null;
    stars: number | null;
    tickets: number;
    hobbies: [];
}