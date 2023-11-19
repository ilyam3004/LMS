import {Group} from "./group";

export interface Subject {
  subjectId: string;
  name: string;
  description: string;
  groups: Group[];
}
