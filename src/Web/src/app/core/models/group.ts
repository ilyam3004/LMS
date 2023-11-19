import {Student} from "./student";

export interface Group {
  groupId: string,
  name: string,
  department: string,
  students: Student[]
}
