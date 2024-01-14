import {Student} from "./student";

export interface GroupsResponse {
  groups: Group[]
}

export interface Group {
  groupId: string,
  name: string,
  department: string,
  students: Student[]
}
