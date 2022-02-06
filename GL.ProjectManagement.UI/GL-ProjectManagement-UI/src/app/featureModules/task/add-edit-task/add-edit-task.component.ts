import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ProjectInfo } from '../../project/shared/models/project-info';
import { ProjectManagementService } from '../../project/shared/services/project-mangement.service';
import { UserInfo } from '../../user/shared/models/user-info.model';
import { UserManagementService } from '../../user/shared/services/user-management.service';
import { Status } from '../shared/models/status.model';
import { TaskInfo } from '../shared/models/task-info.model';
import { TaskManagementService } from '../shared/services/task-mangement.service';

@Component({
  selector: 'app-add-edit-task',
  templateUrl: './add-edit-task.component.html',
  styleUrls: ['./add-edit-task.component.css']
})
export class AddEditTaskComponent implements OnInit {

  editmode = false;
  title = "New Task"
  loading = false;
  submitted = false;
  selectedTaskid = 0;
  form!: FormGroup
  public projects!: Array<ProjectInfo>
  public users!: Array<UserInfo>
  statuses:Status[] = new Array<Status>()
  taskInfo:TaskInfo={
    id: '',
    projectId: 0,
    status: 0,
    assignedToUserId: 0,
    detail: '',
    createdOn: ''
  }

  constructor(private formBuilder: FormBuilder,private taskService:TaskManagementService,
     private projectService:ProjectManagementService, private userService:UserManagementService, private route:ActivatedRoute, private router:Router) {

    this.statuses.push(new Status(0, "New"));
    this.statuses.push(new Status(1, "In progress"));
    this.statuses.push(new Status(2, "Done"));

   }

  get f() { 
    return this.form.controls;
   }

  ngOnInit(): void {
    this.projectService.getAllProjects()
    .subscribe(project=>{ 
      this.projects = JSON.parse(JSON.stringify(project))
    })

    this.userService.getAllUsers()
    .subscribe(users=>{ this.users = JSON.parse(JSON.stringify(users))})

    this.route.queryParams
    .subscribe(params=>{
      if(params.id != undefined){
        this.selectedTaskid = +params.id
        this.taskService.getTask(this.selectedTaskid)
        .subscribe(result=>{
          this.taskInfo = JSON.parse(JSON.stringify(result))
          this.title = "Update Task"
          this.editmode = true
        });
      }     
    });

    this.initialize()

  }

  onDelete(){
    this.taskService.deleteTask(this.selectedTaskid)
    .pipe(first())
    .subscribe({next: (message)=> {
      alert(message);
      this.router.navigate(['../'], { relativeTo: this.route });

    }, 
    error: (res)=>{ 


    }
  });
  }
  
  onSubmit(){

    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    if(this.editmode){
      this.taskService.updateTask(this.selectedTaskid,this.form.value)
      .pipe(first())
      .subscribe({next: (message)=> {
        alert(message);
        this.router.navigate(['../'], { relativeTo: this.route });
      }, 
      error: (res)=>{ 
      }
      });
    }
    else{

      console.log(this.form.value)
      this.taskService.addTask(this.form.value)
      .pipe(first())
      .subscribe({next: (message)=> {
        alert('Task created with id ' + message.id);
        this.router.navigate(['../'], { relativeTo: this.route });
      }, error: ()=>{ 

      }
    });
    }

  }

  initialize(){
    this.form = this.formBuilder.group({     
      projectId: ['', Validators.required],
      userid:['', Validators.required],
      status:['', Validators.required],
      detail:['', Validators.required]
      
  });
  }

}
