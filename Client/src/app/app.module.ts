import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RegisterComponent } from './user/register/register.component';
import { LoginComponent } from './user/login/login.component';
import { UserService } from './services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RootNavComponent } from './root-nav/root-nav.component';
import { AtmMenuComponent } from './atm-menu/atm-menu.component';
import { BankingOperatorComponent } from './banking-operator/banking-operator.component';
import { AuthenticationComponent } from './banking-operator/authentication/authentication.component';
import { ClientService } from './services/client.service';
import { BankingOperatorService } from './services/banking-operator.service';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    RootNavComponent,
    AtmMenuComponent,
    BankingOperatorComponent,
    AuthenticationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    UserService,
    ClientService,
    BankingOperatorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
