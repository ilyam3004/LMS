export class User {
    userId?: string;
    email?: string;
    token?: string;
}

export interface RegisterLecturerRequest {
    email: string,
    password: string,
    firstName: string,
    lastName: string,
    degree?: string,
    birdthay: Date,
    address: string
}

export interface RegisterStudentRequest {
    email: string,
    password: string,
    firstName: string,
    lastName: string,
    groupName: string,
    course: number,
    birdthay: Date,
}