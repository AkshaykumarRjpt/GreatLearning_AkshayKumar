import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { TaskInfoComponent } from './task-info/task-info.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { TaskManagementService } from './shared/services/task-mangement.service';
import { UserManagementService } from '../user/shared/services/user-management.service';
import { ProjectManagementService } from '../project/shared/services/project-mangement.service';
import { AddEditTaskComponent } from './add-edit-task/add-edit-task.component';

const ROUTES = [
  {path:'', component:TaskInfoComponent},
  {path:'add-edit-task', component: AddEditTaskComponent}
];

@NgModule({
  declarations: [
    TaskInfoComponent,
    AddEditTaskComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES),
    FontAwesomeModule,
    ReactiveFormsModule
  ],
  providers:[
    TaskManagementService,
    UserManagementService,
    ProjectManagementService
  ]
})
export class TaskModule { }
