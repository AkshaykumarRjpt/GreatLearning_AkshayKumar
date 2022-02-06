import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { ProjectInfo } from '../shared/models/project-info';
import { ProjectManagementService } from '../shared/services/project-mangement.service';

@Component({
  selector: 'app-project-info',
  templateUrl: './project-info.component.html',
  styleUrls: ['./project-info.component.css']
})
export class ProjectInfoComponent implements OnInit {

  faPlusCircle = faPlusCircle;
  
  public projects!: Array<ProjectInfo>
  constructor(private projectService:ProjectManagementService, private router:Router) { }

  ngOnInit(): void {
     this.projectService.getAllProjects()
     .subscribe(result=>{
      this.projects = JSON.parse(JSON.stringify(result)); 
     });
    }

    editProject(id:number){

    
      this.router.navigate(["projects-info/add-edit-project"], { queryParams: { id } });
    }
}