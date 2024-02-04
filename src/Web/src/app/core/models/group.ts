import {Student} from "./student";

export interface GroupsResponse {
  groups: Group[]
}

export interface Group {
  groupId: string,
  name: string,
  course: number,
  department: string,
  students: Student[]
}

export interface GroupProfile {
  groupId: string,
  name: string,
  department: string,
  course: number,
}
