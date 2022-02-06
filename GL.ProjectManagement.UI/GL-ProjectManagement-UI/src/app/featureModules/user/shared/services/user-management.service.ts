import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserInfo } from "../models/user-info.model";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { environment } from "src/environments/environment";

@Injectable()
export class UserManagementService{
    constructor(private http: HttpClient, private authservice: AuthenticationService){

    }
    
    getAllUsers()
    {        
        return this.http.get(`${environment.apiUrl}/User`);
    }

    getUser(id: number)
    {        
        return this.http.get(`${environment.apiUrl}/User/${id}`);
    }

    addUser(user:any){
        return this.http.post<any>(`${environment.apiUrl}/User`, {email: user.email,firstName:user.firstName, lastName: user.lastName, password: user.password})      
    }
    
    updateUser(id:number , user:any){
      
        return this.http.put(`${environment.apiUrl}/User`, 
        {id:id, email: user.email,firstName:user.firstName, lastName: user.lastName, password: user.password},   { responseType: 'text'}) 
    }

    deleteUser(id:number){
        return this.http.delete(`${environment.apiUrl}/User/${id}`,   { responseType: 'text'}) 
    }

    
}