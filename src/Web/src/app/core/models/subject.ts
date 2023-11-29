import {Group} from "./group";
import {LecturerTask, StudentTask, StudentTasks, UploadedStudentTask} from "./task";

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
