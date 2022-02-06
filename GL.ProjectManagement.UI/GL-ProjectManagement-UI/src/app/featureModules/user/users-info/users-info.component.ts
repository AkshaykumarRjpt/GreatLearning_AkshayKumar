import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../shared/models/user-info.model';
import { UserManagementService } from '../shared/services/user-management.service';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users-info',
  templateUrl: './users-info.component.html',
  styleUrls: ['./users-info.component.css']
})
export class UsersInfoComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  iconText = "New User"
  public users!: Array<UserInfo>
  constructor(private UserService:UserManagementService, private router:Router) { }

  ngOnInit(): void {
     this.UserService.getAllUsers()
     .subscribe(result=>{
      this.users = JSON.parse(JSON.stringify(result)); 
     });
    

    }

    editUser(id:number){
      this.router.navigate(["users-info/add-edit-user"], { queryParams: { id } });
    }
}
