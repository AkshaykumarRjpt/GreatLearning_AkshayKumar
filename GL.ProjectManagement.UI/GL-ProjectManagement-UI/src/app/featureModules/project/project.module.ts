import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProjectInfoComponent } from './project-info/project-info.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProjectManagementService } from './shared/services/project-mangement.service';
import { AddEditProjectComponent } from './add-edit-project/add-edit-project.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



const ROUTES = [
  { path: '', component: ProjectInfoComponent},
  { path: 'add-edit-project', component: AddEditProjectComponent}
];

@NgModule({
  declarations: [
    ProjectInfoComponent,
    AddEditProjectComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES),
    FontAwesomeModule,
    ReactiveFormsModule,
     
    
  ],
  providers:[
    ProjectManagementService
  ]
})
export class ProjectModule { }
