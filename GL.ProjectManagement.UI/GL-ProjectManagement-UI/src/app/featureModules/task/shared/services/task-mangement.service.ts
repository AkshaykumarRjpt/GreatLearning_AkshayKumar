import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { environment } from "src/environments/environment";

@Injectable()
export class TaskManagementService{
    constructor(private http: HttpClient){

    }

    getAllTasks()
    {        
        return this.http.get(`${environment.apiUrl}/Task`);
    }

    getTask(id: number)
    {        
        return this.http.get(`${environment.apiUrl}/Task/${id}`);
    }

    addTask(task:any){
        return this.http.post<any>(`${environment.apiUrl}/Task`, {projectId: task.projectId, status:task.status, assignedToUserId: task.userid, detail: task.detail})      
    }
    
    updateTask(id:number , task:any){
      
        return this.http.put(`${environment.apiUrl}/Task`, 
        {id:id, projectId: task.projectId, status:task.status, assignedToUserId: task.userid, detail: task.detail},   { responseType: 'text'}) 
    }

    deleteTask(id:number){
        return this.http.delete(`${environment.apiUrl}/Task/${id}`,   { responseType: 'text'}) 
    }
}