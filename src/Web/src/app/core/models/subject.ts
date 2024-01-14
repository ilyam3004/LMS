import {LecturerTask, StudentTask, StudentTasks} from "./task";
import {Group} from "./group";

export interface LecturerSubjectsResponse {
  subjects: LecturerSubject[];
}

export interface LecturerSubject {
  subjectId: string;
  name: string;
  description: string;
  group: Group;
  tasks: LecturerTask[];
}

export interface StudentSubjectsResponse {
  subjects: StudentSubject[];
}

export interface StudentSubject {
  subjectId: string,
  name: string,
  description: string,
  lecturerName: string,
  tasks: StudentTask[],
  totalGrade: number,
  averageGrade: number,
}

export interface SubjectGrades {
  subjectId: string,
  subjectName: string,
  groupName: string,
  studentTasks: StudentTasks[]
}

export interface CreateSubjectRequest {
  name: string;
  description: string;
  group: string;
}
