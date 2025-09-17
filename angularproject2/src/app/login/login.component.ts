import { Component } from '@angular/core';
import { LoginService } from '../login.service';
import { Login } from '../login';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  login:Login=new Login();
  constructor(private loginService:LoginService,private router:Router){}
  loginClick()
  {
    debugger
    this.loginService.checkUser(this.login).subscribe(
      (response)=>{
        this.router.navigateByUrl("/home");

      },
      (error)=>{
        alert("wrong user/pwd");
        this.login.name="";
        this.login.password="";
      }
    )
  }

}
