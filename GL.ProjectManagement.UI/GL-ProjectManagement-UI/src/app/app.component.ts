import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './shared/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'GL-ProjectManagement-UI';

  constructor(private authService:AuthenticationService, private router: Router){

  }
  ngOnInit(): void {
    // if(this.isLoggedIn())
    // {
    //   this.router.navigate(['users-info']);
    // }
  }

  isLoggedIn(){

    return this.authService.isLoggedIn()
  }
  
}
