import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './login.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class ActivateguardService implements CanActivate {

  constructor(private loginService:LoginService,private router:Router,private jwtHelperService:JwtHelperService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    if(this.loginService.isAuthenticated())
    {
      return true;
    }
    else
    {
      alert('you are not authorized to access this information !!!');
      this.router.navigateByUrl("/login");
      return false;
    }
  }
}
