import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public isLoggedIn$: BehaviorSubject<boolean>; 

  constructor(private fb:FormBuilder, private http:HttpClient, private router:Router) 
  {
    const isLoggedIn = localStorage.getItem('loggedIn') === 'true';
    this.isLoggedIn$ = new BehaviorSubject(isLoggedIn);
  }

  readonly BaseURI = 'http://localhost:57328/api';


  formModel = this.fb.group({
    UserName :['',Validators.required],
    Passwords : this.fb.group({
      Password :['',[Validators.required,Validators.minLength(4)]],
      ConfirmPassword :['',Validators.required]
    },{validator : this.comparePasswords}),
    Email :['',Validators.email],
    FirstName :[''],
    LastName :[''],
    CNP :[''],
    Address :[''],
    PhoneNumber :[''],
    Role :['']
  });


  comparePasswords(fb:FormGroup)
  {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if(confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors)
    {
      if(fb.get('Password').value != confirmPswrdCtrl?.value)
      confirmPswrdCtrl.setErrors({passwordMismatch: true});
      else
      confirmPswrdCtrl.setErrors(null);
    }
  }

  register()
  {
    var body = {
      UserName: this.formModel.value.UserName,
      Password: this.formModel.value.Passwords.Password,
      Email: this.formModel.value.Email,
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName,
      CNP: this.formModel.value.CNP,
      Address: this.formModel.value.Address,
      PhoneNumber: this.formModel.value.PhoneNumber,
      Role: this.formModel.value.Role
    };  

    return this.http.post(this.BaseURI+'/ApplicationUser/Register',body);
  }

  login(formData: any)
  {
    return this.http.post(this.BaseURI+'/ApplicationUser/Login',formData);
  }

  
  addRoles() {
    return this.http.post(this.BaseURI+'/Administration/CreateRole', null);
  }

  logout()
  {
    localStorage.setItem('loggedIn', 'false');
    this.isLoggedIn$.next(false);

    localStorage.removeItem('token');
  }

  getUserName()
  {
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer '+localStorage.getItem('token')})
    return this.http.get(this.BaseURI+'/UserProfile',{headers : tokenHeader});
  }

  isLogged(): boolean
  {
    return this.isLoggedIn$.value;
  }

}
