export interface Task {
  taskId: string,
  title: string,
  description: string,
  deadline: Date,
  createdAt: Date,
  maxGrade: number
}

export interface AssignTaskRequest {
  title: string,
  description: string,
  subjectId: string,
  deadline: Date,
  maxGrade: number
}
