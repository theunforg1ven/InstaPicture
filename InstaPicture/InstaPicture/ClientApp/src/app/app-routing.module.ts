import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PhotoinstaComponent } from './photoinsta/photoinsta.component';
import { PicsaveComponent } from './picsave/picsave.component';
import { UsersaveComponent } from './usersave/usersave.component';

const routes: Routes = [
  { path: '', redirectTo: '/photoinsta', pathMatch: 'full' },
  { path: 'photoinsta', component: PhotoinstaComponent },
  { path: 'picsave', component: PicsaveComponent },
  { path: 'usersave', component: UsersaveComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
