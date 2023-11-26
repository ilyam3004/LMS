import {Group} from "./group";
import {LecturerTask} from "./task";

export interface LecturerSubject {
  subjectId: string;
  name: string;
  description: string;
  group: Group;
  tasks: LecturerTask[];
}

export interface StudentSubject {
  subjectId: string,
  name: string,
  description: string,
  lecturerName: string,
  tasks: LecturerTask[]
}

export interface CreateSubjectRequest {
  name: string;
  description: string;
  group: string;
}
