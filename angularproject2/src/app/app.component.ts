import { Component } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'angularproject2';
  constructor(public loginservice:LoginService,private router:Router){}
  Logout()
  {
    sessionStorage.clear();
    this.router.navigateByUrl("/login");
  }
}
