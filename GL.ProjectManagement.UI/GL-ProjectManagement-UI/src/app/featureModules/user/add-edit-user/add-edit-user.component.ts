import { Component, OnInit, ɵCompiler_compileModuleAndAllComponentsAsync__POST_R3__ } from '@angular/core';
  import { EmailValidator, FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ConfirmedValidator } from 'src/app/shared/validators/confirmMatch.validator';

import { User } from 'src/app/_models/User';
import { UserInfo, Email } from '../shared/models/user-info.model';
import { UserManagementService } from '../shared/services/user-management.service';



@Component({
  templateUrl: './add-edit-user.component.html',
  styleUrls: ['./add-edit-user.component.css']
})
export class AddEditComponent implements OnInit {

  constructor(private routeparams: ActivatedRoute, 
    private UserService:UserManagementService,  
    private formBuilder: FormBuilder,
    private router:Router,
    private route: ActivatedRoute) { }
  
  selecteduserid = 0;
  editmode = false
  userinfo = "New User"
  form!: FormGroup
  submitted = false
  loading = false;
  confirmedpass =""
  user = {
    firstName: '',
    lastName: '',
    email: '',
    id: 0,
    password:''
  } 
  
  ngOnInit(): void {
     this.routeparams.queryParams
     .subscribe(
       params=> {
        if(params.id != undefined)
        {
          this.selecteduserid = +params.id
          this.UserService.getUser( this.selecteduserid).subscribe(result=>{
            this.user = JSON.parse(JSON.stringify(result)); 
            this.userinfo = this.user.firstName + " "+ this.user.lastName;
            this.confirmedpass = this.user.password
            this.editmode = true
           });
        }
       }
     );

     this.initialize();
  }

  get f() { 
    return this.form.controls;
   }

  onDelete(){
      if(confirm("Are you sure you want to Delete "+ this.user.firstName)) {
      this.UserService.deleteUser(this.selecteduserid)
      .pipe(first())
      .subscribe({next: (message)=> {
        alert(message);
        this.router.navigate(['../'], { relativeTo: this.route });
      }, 
      error: (res)=>{ 
        alert(res.error);
      }});
    }
  }


  onSubmit(){
      this.submitted = true;

      if (this.form.invalid) {
        return;
      }

      if(this.editmode){
        if(confirm("Are you sure you want to update "+this.user.firstName)) {
            this.UserService.updateUser(this.selecteduserid, this.form.value)
            .pipe(first())
            .subscribe({next: (message)=> {
              alert("User " + message);
              this.router.navigate(['../'], { relativeTo: this.route });

            }, 
            error: (res)=>{ 
            }});
        }
      }
      else{
          this.UserService.addUser(this.form.value)
          .pipe(first())
          .subscribe({next: (message)=> {
            alert('User Created with id ' + message.id);
            this.router.navigate(['../'], { relativeTo: this.route });
          }, 
          error: ()=>{ 
          }});
      }
  }

  initialize(){
    this.form = this.formBuilder.group({     
      firstName: ['', Validators.required],
      lastName: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.minLength(6), this.editmode ? Validators.nullValidator:  Validators.required ]],
      confirmPassword: ['', this.editmode ? Validators.nullValidator:  Validators.required]
      }, {
          validator: ConfirmedValidator('password','confirmPassword' )
      });
  }
}
