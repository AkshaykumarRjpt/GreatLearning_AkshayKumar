import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/_models/User';
import { ProjectInfo } from '../../project/shared/models/project-info';
import { ProjectManagementService } from '../../project/shared/services/project-mangement.service';
import { UserInfo } from '../../user/shared/models/user-info.model';
import { UserManagementService } from '../../user/shared/services/user-management.service';
import { Status } from '../shared/models/status.model';
import { TaskDisplayInfo } from '../shared/models/task-display-info';
import { TaskInfo } from '../shared/models/task-info.model';
import { TaskManagementService } from '../shared/services/task-mangement.service';

@Component({
  selector: 'app-task-info',
  templateUrl: './task-info.component.html',
  styleUrls: ['./task-info.component.css']
})
export class TaskInfoComponent implements OnInit {

  faPlusCircle = faPlusCircle;
  public tasks!: Array<TaskInfo>
  public users!: Array<UserInfo>
  public projects!: Array<ProjectInfo>
  displayTasks: TaskDisplayInfo[] = new Array<TaskDisplayInfo>();
  statuses:Status[] = new Array<Status>()
 
  
  constructor(private taskService:TaskManagementService, private userService:UserManagementService, private projectService:ProjectManagementService,
    private router:Router) {

        this.statuses.push(new Status(0, "New"));
        this.statuses.push(new Status(1, "In progress"));
        this.statuses.push(new Status(2, "Done"));
   }

  ngOnInit(): void {

    

    this.taskService.getAllTasks()
    .subscribe(result=>{
     this.tasks = JSON.parse(JSON.stringify(result)); 
     if(this.tasks != null)
     {
       this.userService.getAllUsers()
       .subscribe(users=> {
         if(users != null){
          this.users = JSON.parse(JSON.stringify(users));
          this.projectService.getAllProjects()
          .subscribe(projects=>{
            this.projects = JSON.parse(JSON.stringify(projects)); 

            this.tasks.forEach(task => {
              let userName = this.users.find(x=>x.id == task.assignedToUserId)?.firstName??''
              let projectName = this.projects.find(x=>x.id == task.projectId.toString())?.name??''
              let status = this.statuses.find(x=>x.id == task.status)?.name??''
              this.displayTasks.push(new TaskDisplayInfo(task.id.toString(), projectName, task.detail, status, userName, task.createdOn))
               
             });
          })
         }
       })
     }
    });

   

  }

  editask(id:number){
    this.router.navigate(["tasks-info/add-edit-task"], { queryParams: { id } });
  }

 
}
