import {Group} from "./group";

export interface Subject {
  subjectId: string;
  name: string;
  description: string;
  groups: Group[];
}

export interface CreateSubjectRequest {
  name: string;
  description: string;
  group: string;
}
