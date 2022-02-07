import { Component, OnInit } from '@angular/core';
import { EmailValidator, FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { first } from 'rxjs/operators';
import { ProjectInfo } from '../shared/models/project-info';
import { ProjectManagementService } from '../shared/services/project-mangement.service';

@Component({
  selector: 'app-add-edit-project',
  templateUrl: './add-edit-project.component.html',
  styleUrls: ['./add-edit-project.component.css']
})
export class AddEditProjectComponent implements OnInit {

  editmode = false;
  loading = false;
  title = 'New Project';
  form!: FormGroup
  selectedProjectid = 0
  submitted = false;

  projectInfo:ProjectInfo ={
    id: '',
    name: '',
    detail: '',
    createdOn: new Date()
  }
 
  constructor(private route:ActivatedRoute, private router:Router, private formBuilder: FormBuilder, private projectservice:ProjectManagementService) { }

  ngOnInit(): void {
    this.route.queryParams
    .subscribe(params=>{
      if(params.id != undefined){
        this.selectedProjectid = +params.id
        this.projectservice.getProject(this.selectedProjectid)
        .subscribe(result=>{
          this.projectInfo = JSON.parse(JSON.stringify(result))
          this.title = this.projectInfo.name
          this.editmode = true
        });
      }     
    });

    this.initialize();
  }

  onDelete(){
      if(confirm("Are you sure you want to Delete "+this.projectInfo.name)) {
      this.projectservice.deleteProject(this.selectedProjectid)
      .pipe(first())
      .subscribe({next: (message)=> {
        alert(message);
        this.router.navigate(['../'], { relativeTo: this.route });

      }, 
      error: (res)=>{ 
        alert(res.error);}
      });
    } 
  }

  onSubmit(){
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    if(this.editmode){
        if(confirm("Are you sure you want to update "+this.projectInfo.name)) {
        this.projectservice.upadteProject(this.selectedProjectid,this.form.value)
        .pipe(first())
        .subscribe({next: (message)=> {
          alert("Project " + message);
          this.router.navigate(['../'], { relativeTo: this.route });
        }, 
        error: (res)=>{ 
        }
        });
      }
    }
    else{

      console.log(this.form.value)
      this.projectservice.createProject(this.form.value)
      .pipe(first())
      .subscribe({next: (message)=> {
        alert('project created with id ' + message.id);
        this.router.navigate(['../'], { relativeTo: this.route });
      }, error: ()=>{ 

      }
    });
    }
    
     
  }

  get f() { 
    return this.form.controls;
   }

  initialize(){
    this.form = this.formBuilder.group({     
      name: ['', Validators.required],
      detail:['', Validators.required]
  });
  }


}
