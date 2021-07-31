import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public service: UserService) { }

  ngOnInit(): void {
    this.service.formModel.reset();
  }

  onSubmit()
  {
    this.service.register().subscribe(
      (res:any) => {
        if(res.succeeded){
          this.service.formModel.reset();
          //this.toastr.success('New user created!','Registration successful.')
        }
        else
        {
          res.errors.forEach((element: { code: any; description: string | undefined; }) => {
            switch(element.code){
              case 'DuplicateUserName':
                //this.toastr.error('Username is already taken','Registration failed.');
                //Username is already taken
                break;

                default:
                  //this.toastr.error(element.description,'Registration failed.');
                  //Registration failed
                  break;
            }
          });
        }
      },
      (err: any) => {
        console.log(err);
      }
    );
  }
}
