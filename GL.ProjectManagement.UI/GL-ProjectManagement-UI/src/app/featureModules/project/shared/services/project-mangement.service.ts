import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { environment } from "src/environments/environment";

@Injectable()
export class ProjectManagementService{

    constructor(private httpClient: HttpClient, private authenticationService: AuthenticationService) {

    }

    getAllProjects() {
       
        return this.httpClient.get(`${environment.apiUrl}/Project`);
    }

    getProject(id:number) {
       
        return this.httpClient.get(`${environment.apiUrl}/Project/${id}`);
    }

    createProject(project:any){
        return this.httpClient.post<any>(`${environment.apiUrl}/Project`, {name: project.name, detail:project.detail });
    }

    upadteProject(id:number, project:any){

        return this.httpClient.put(`${environment.apiUrl}/Project`, {id: id, name: project.name, detail:project.detail },  { responseType: 'text'});
    }

    deleteProject(id:number){
        return this.httpClient.delete(`${environment.apiUrl}/Project/${id}`,  { responseType: 'text'});
    }

}