import { Component, OnInit } from '@angular/core';
import { PhotoinstaService } from '../shared/photoinsta.service';

@Component({
  selector: 'app-photoinsta',
  templateUrl: './photoinsta.component.html',
  styleUrls: ['./photoinsta.component.scss']
})
export class PhotoinstaComponent implements OnInit {
  divIndex: number[] = [];

  constructor(private service: PhotoinstaService) { }

  ngOnInit() {
    this.service.savedStoriesList = [];
    this.service.list = [];
  }

  async onGetCurrentStories(username: string) {
    this.divIndex = [];

    // clear "Saved Stories" and "Current Stories" if finding new user stories
    this.service.savedStoriesList = null;
    this.service.list = [];

    if (username != null) {
      this.service.username = username;
    }

    await this.service.getCurrentStories().then(
      () => {
        this.service.formatDateToLocal();
        if (this.service.list[0].Uri === 'There are no stories') {
          this.service.list = null;
        }
      },
      err => {
        console.log(err);
      });

    await this.service.getSavedStories().then(
      () => {
        this.service.formatArchiveDateToLocal();
        if (this.service.savedStoriesList[0].UnifyStoryName === 'Error') {
          this.service.savedStoriesList = null;
        }
      },
      err => {
        console.log(err);
      });
  }

  onInstaClick(mention: string) {
    const mentionUser = mention.substring(1);
    const url = `https://instagram.com/${mentionUser}`;
    window.open(url, '_blank');
  }

  onInstaArchiveToggle(index: number) {
    if (this.divIndex[index] !== index) {
      this.divIndex[index] = index;
    } else {
      this.divIndex[index] = -1;
    }
  }

  ifInclude(url: string): boolean {
    if (url.includes('/vp/')) {
      return true;
    }
    return false;
  }
}
