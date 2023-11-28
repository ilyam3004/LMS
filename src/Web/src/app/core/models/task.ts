import {Student} from "./student";

export interface LecturerTask {
  taskId: string,
  title: string,
  description: string,
  deadline: Date,
  createdAt: Date,
  maxGrade: number
  groupName: string,
  studentTasks: UploadedStudentTask[]
}

export interface StudentTask {
  taskId: string,
  title: string,
  description: string,
  deadline: Date,
  createdAt: Date,
  maxGrade: number
  lecturerName: string,
  uploadedTask: UploadedStudentTask
}

export interface UploadedStudentTask {
  studentTaskId: string,
  taskId: string,
  fileUrl: string | null,
  uploadedAt: Date | null,
  grade: number,
  status: StudentTaskStatus
  student: Student,
  comments: TaskComment[],
}

export interface TaskComment {
  taskCommentId: string,
  userId: string,
  comment: string,
  createdAt: Date,
  username: string
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
  Returned,
  Accepted,
  NotUploaded,
  Rejected
}
