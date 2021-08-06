import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AtmMenuComponent } from './atm-menu/atm-menu.component';
import { AuthenticationComponent } from './banking-operator/authentication/authentication.component';
import { BankingOperatorComponent } from './banking-operator/banking-operator.component';
import { HasRoleGuard } from './has-role.guard';
import { HomeComponent } from './home/home.component';
import { IsAuthenticatedGuard } from './is-authenticated.guard';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path:'', redirectTo:'/home', pathMatch:'full' },
  { path:'home', component: HomeComponent},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  { 
    path:'atm-menu', 
    component: AtmMenuComponent,
    canActivate: [IsAuthenticatedGuard, HasRoleGuard],
    data: {
      role: 'Client',
    }
  },
  { 
    path:'banking-operator', 
    component: BankingOperatorComponent,
    canActivate: [IsAuthenticatedGuard, HasRoleGuard],
    data: {
      role: 'Operator Bancar',
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
