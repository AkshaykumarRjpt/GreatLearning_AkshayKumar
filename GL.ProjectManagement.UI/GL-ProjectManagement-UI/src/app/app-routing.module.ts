import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  {path: '', redirectTo:'login', pathMatch:'full'},
  {
    path: 'users-info', loadChildren: () =>
    import('src/app/featureModules/user/user.module')
    .then(m => m.UserModule),
    
  },
  {
    path: 'projects-info', loadChildren: () =>
    import('src/app/featureModules/project/project.module')
    .then(m => m.ProjectModule)
  },
  {
    path: 'tasks-info', loadChildren: () =>
    import('src/app/featureModules/task/task.module')
    .then(m => m.TaskModule)
  },
 {path: 'login', component: LoginComponent, canActivate:[AuthGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
