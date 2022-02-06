import { Component } from "@angular/core";
import { AuthenticationService } from "../../services/authentication.service";
//import { AuthenticationService } from "../shared/services/authentication.service";

@Component({
    selector: 'app-sidenav',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
    constructor(private authservice:AuthenticationService) {

    }
    isLoggedIn() {
      return this.authservice.isLoggedIn()
    }
}