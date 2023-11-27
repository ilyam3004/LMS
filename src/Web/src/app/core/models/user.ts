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
  birthday: string,
  address: string
}

export interface RegisterStudentRequest {
  email: string,
  password: string,
  firstName: string,
  lastName: string,
  groupName: string,
  course: number,
  birthday: string,
}

export interface StudentProfile {
  userId: string,
  email: string,
  fullName: string,
  group: string,
  course: string,
  birthday: string,
  address: string
}

export interface LecturerProfile {
  userId: string,
  email: string,
  fullName: string,
  degree: string,
  birthday: string,
  address: string
}
