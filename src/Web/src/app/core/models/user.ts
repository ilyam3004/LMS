import {Group, GroupProfile, GroupsResponse} from "./group";

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
  birthday: string,
  address: string
}

export interface StudentProfile {
  userId: string,
  email: string,
  fullName: string,
  groupProfile: GroupProfile,
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
