import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersInfoComponent } from './users-info/users-info.component';
import { Router, RouterModule } from '@angular/router';
import { UserManagementService } from './shared/services/user-management.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AddEditComponent } from './add-edit-user/add-edit-user.component'
import { ReactiveFormsModule } from '@angular/forms';

const ROUTES = [
  { path: '', component: UsersInfoComponent},
  { path: 'add-edit-user', component: AddEditComponent}
  
];

@NgModule({
  declarations: [
    UsersInfoComponent,
    AddEditComponent
  ],
  imports: [
    RouterModule.forChild(ROUTES),
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule
  ],
  providers:[
    UserManagementService
  ]
})
export class UserModule { }
