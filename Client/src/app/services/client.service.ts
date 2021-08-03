import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private fb:FormBuilder, private http:HttpClient, private router:Router) { }

  readonly BaseURI = 'http://localhost:63086/api';

}
