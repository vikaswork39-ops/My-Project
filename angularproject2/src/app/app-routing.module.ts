import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { SupportComponent } from './support/support.component';
import { ContactComponent } from './contact/contact.component';
import { EmployeeComponent } from './employee/employee.component';
import { LoginComponent } from './login/login.component';
import { ActivateguardService } from './activateguard.service';

const routes: Routes = [
  {path:"home",component:HomeComponent},
  {path:"about",component:AboutComponent},
  {path:"support",component:SupportComponent},
  {path:"contact",component:ContactComponent,canActivate:[ActivateguardService]},
  {path:"employee",component:EmployeeComponent,canActivate:[ActivateguardService]},
  {path:"login",component:LoginComponent},
  {path:"",redirectTo:"home",pathMatch:"full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
