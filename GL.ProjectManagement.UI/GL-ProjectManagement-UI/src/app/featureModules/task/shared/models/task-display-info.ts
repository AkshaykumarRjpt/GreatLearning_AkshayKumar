export class TaskDisplayInfo {
    id: string;
    projectName: string;
    detail: string
    status: string;
    assignedToUser: string;
    createdOn: string;

    constructor(id:string, projectName:string, detail:string, status:string, assignedToUser: string, createdOn:string){

        this.id = id
        this.projectName = projectName
        this.detail = detail
        this.status = status
        this.assignedToUser = assignedToUser
        this.createdOn = createdOn
    }
}