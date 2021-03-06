import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel={
    UserName : '',
    Password : ''
  }

  constructor(private service:UserService, private router:Router) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null )
      this.router.navigateByUrl('/home');
  }

  onSubmit(form:NgForm){
    this.service.login(form.value).subscribe(
      (res:any)=>{
        localStorage.setItem('token',res.token);
        this.router.navigateByUrl('/atm-menu');
        localStorage.setItem('loggedIn', 'true');
        this.service.isLoggedIn$.next(true);
      },
      err=>{
        if(err.status == 400)
        {
          //this.toastr.error('Nume de utilizator sau parola incorecta.','Autentificare esuata');
          localStorage.setItem('loggedIn','false');
          this.service.isLoggedIn$.next(false);
        }
        else
          console.log(err);
      }
    )
  }

}
