import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class ContactComponent {
  constructor(private router:Router){}
  // ngOnInit()
  // {
  //   if(sessionStorage.getItem("currentUser")==null)
  //   {
  //     alert('you are not able to accessthis information!!');
  //     this.router.navigateByUrl("/login");
      
  //   }
  // }

}
