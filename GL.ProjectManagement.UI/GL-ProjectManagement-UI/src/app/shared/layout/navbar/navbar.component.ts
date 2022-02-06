import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "../../services/authentication.service";

@Component({
    selector:'nav-bar',
    templateUrl:'./navbar.component.html'

})
export class NavBarComponent{
    userName: string = ""
    constructor(private authService: AuthenticationService, private router:Router){

    }

    isLoggedIn(){
       return this.authService.isLoggedIn()
    }

    getUserName()
    {
        return this.authService.getUsername();
    }

    logout(){
        this.authService.logout();       
        this.router.navigate(['/login']);
    }
    
}