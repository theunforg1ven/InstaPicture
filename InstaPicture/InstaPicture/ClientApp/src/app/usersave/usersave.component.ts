import { Component, OnInit } from '@angular/core';
import { PhotoinstaService } from '../shared/photoinsta.service';

@Component({
  selector: 'app-usersave',
  templateUrl: './usersave.component.html',
  styleUrls: ['./usersave.component.scss']
})
export class UsersaveComponent implements OnInit {

  constructor(private service: PhotoinstaService) { }

  ngOnInit() {
    this.service.currentUser = null;
  }

  async onGetCurrentUser(username: string) {
    if (username != null) {
      this.service.curUsername = username;
    }
    await this.service.getCurrentUserInfo().then(
      () => {
        if (this.service.currentUser.UserName === 'No such user') {
          this.service.currentUser = null;
        }
      },
      err => {
        console.log(err);
      });
  }

  onInstaClick(extUrl: string) {
    const url = extUrl;
    (window as any).open(url, '_blank');
  }

  getFromBool(value: boolean): string {
    if (value === true) {
      return 'Yeap';
    }
    return 'Nope';
  }

}
