import {Student} from "./student";

export interface LecturerTask {
  taskId: string,
  title: string,
  description: string,
  deadline: Date,
  createdAt: Date,
  maxGrade: number
  groupName: string,
  studentTasks: StudentTask[]
}

export interface StudentTask {
  studentTaskId: string,
  taskId: string,
  fileUrl: string | null,
  uploadedAt: Date | null,
  grade: number,
  status: StudentTaskStatus
  student: Student
}

export interface AssignTaskRequest {
  title: string,
  description: string,
  subjectId: string,
  deadline: string | null,
  maxGrade: number
}

export enum StudentTaskStatus {
  Uploaded,
  Rejected,
  Accepted,
  NotUploaded
}
