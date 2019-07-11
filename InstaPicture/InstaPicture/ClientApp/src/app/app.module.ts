import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http'; 
import { DatePipe } from '@angular/common';

import { AppComponent } from './app.component';

import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { PhotoinstaComponent } from './photoinsta/photoinsta.component';
import { PicsaveComponent } from './picsave/picsave.component';
import { UsersaveComponent } from './usersave/usersave.component';

@NgModule({
  declarations: [
    AppComponent,
    PhotoinstaComponent,
    PicsaveComponent,
    UsersaveComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MDBBootstrapModule.forRoot(),
    FormsModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
