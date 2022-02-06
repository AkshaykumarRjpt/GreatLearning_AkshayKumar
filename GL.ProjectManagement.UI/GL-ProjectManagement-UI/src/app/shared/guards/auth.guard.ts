import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthenticationService } from "../services/authentication.service";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate{
   
   constructor(private router:Router, private authservice:AuthenticationService){

   }
   
   
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const isLoggedIn = this.authservice.isLoggedIn();
        
        if(!isLoggedIn){
           
            return true
        }

        this.router.navigate(['/users-info']);
        return false;
        

    }


}