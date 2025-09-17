import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from './login';
import { map, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  currentUserName:string="";


  constructor(private httpclient:HttpClient,private jwtHelperService:JwtHelperService) { }
  checkUser(login:Login):Observable<any>
  {
    return this.httpclient.post<any>("https://localhost:7140/api/user/Authenticate",login).pipe(map(user=>{
      if(user)
      {
        this.currentUserName=user.username;
        sessionStorage['currentUser']=JSON.stringify(user);
      }
      return null;
    }))
  }
  public isAuthenticated():boolean
  {
    if(this.jwtHelperService.isTokenExpired())
    {
      return false;
    }
    else
    {
      return true;
    }
  }

}
