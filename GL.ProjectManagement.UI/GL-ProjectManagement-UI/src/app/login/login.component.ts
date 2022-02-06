import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../shared/services/authentication.service';

@Component({ templateUrl: './login.component.html' })
export class LoginComponent {
  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  returnUrl!: string;
  error = '';

  
  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService) { 

  }

  get f() { return this.loginForm.controls; }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
        email: ['', Validators.required],
        password: ['', Validators.required]
    });
  }

  onSubmit(){
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    
    this.authenticationService.login(this.f.email.value, this.f.password.value)
    .pipe(first())
    .subscribe(
        data => {
            this.router.navigate(['users-info']);
        },
        error => {
            this.error = error;
            this.loading = false;
        });
  }

}   


