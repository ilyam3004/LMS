import {Group} from "./group";
import {Task} from "./task";

export interface Subject {
  subjectId: string;
  name: string;
  description: string;
  groups: Group[];
  tasks: Task[];
}

export interface CreateSubjectRequest {
  name: string;
  description: string;
  group: string;
}
