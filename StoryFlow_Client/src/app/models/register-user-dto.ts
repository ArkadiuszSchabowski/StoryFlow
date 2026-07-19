import { Gender } from "../enums/gender";

export interface RegisterUserDto {
    email: string;
    password: string;
    repeatPassword: string;
    firstName: string | null;
    nick: string | null;
    gender: Gender | null;
    dateOfBirth: string | null;
}