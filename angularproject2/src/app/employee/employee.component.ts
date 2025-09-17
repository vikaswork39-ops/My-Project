import { Component } from '@angular/core';
import { Employee } from '../employee';
import { EmployeeService } from '../employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee',
  standalone: false,
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.scss'
})
export class EmployeeComponent {
  employeesList:Employee[]=[];
  newemployee:Employee=new Employee();
  editEmployee:Employee=new Employee();
  constructor(private employeeService:EmployeeService,private router:Router){}
  getAll()
  {
    this.employeeService.getEmployees().subscribe(
      (response)=>{
        this.employeesList=response;
      },
      (error)=>{
        console.log('unable to access api!!');
      }
    )
  }
  ngOnInit()
  {
    // if(sessionStorage.getItem('currentUser')==null)
    // {
    //   alert('you are not able to accessthis information!!');
    //   this.router.navigateByUrl("/login");
    // }
    this.getAll();
  }
  saveClick()
  {
    this.employeeService.saveEmployee(this.newemployee).subscribe(
      (response)=>{
        this.getAll();
        this.newemployee.name="";
        this.newemployee.address="";
        this.newemployee.salary=0;
      },
      (error)=>{
        console.log('unable to access api!!');
      },
    )
  }
  editClick(data:any)
  {
    this.editEmployee=data;
  }
  updateClick()
  {
    this.employeeService.updateEmployee(this.editEmployee).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log('unable to access api!!');
      },
    )
  }
  deleteClick(id:number)
  {
    let ans=window.confirm('want to delete data?');
    if(!ans) return;
    this.employeeService.deleteEmployee(id).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        alert('unable to access api!!');
      },
    )
  }

}
