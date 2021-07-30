import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-atm-menu',
  templateUrl: './atm-menu.component.html',
  styleUrls: ['./atm-menu.component.css']
})
export class AtmMenuComponent implements OnInit {

  constructor(private service: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  onLogout()
  {
    //this.toastr.success('V-ati deconectat cu succes.','Deconectare');
    this.service.logout();
    return this.router.navigate(['/home']);
  }
}
