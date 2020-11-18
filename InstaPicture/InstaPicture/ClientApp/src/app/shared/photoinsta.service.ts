import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurStory } from '../models/curstory.model';
import { DatePipe } from '@angular/common';
import { SavedStory } from '../models/savedstory.model';
import { CurUser } from '../models/curuser.model';

@Injectable({
  providedIn: 'root'
})
export class PhotoinstaService {
  username: string;
  pictureLink: string;
  curUsername: string;

  list: CurStory[];
  savedStoriesList: SavedStory[];
  fullSizePictures: string[];
  currentUser: CurUser;

  readonly rootUrl = 'https://localhost:44338/api';

  constructor(private http: HttpClient,
              private datepipe: DatePipe) { }

  async getCurrentStories() {
    const res = await this.http.get(this.rootUrl + '/media/stories?username=' + this.username)
      .toPromise();
    return this.list = (res as CurStory[]);
  }

  async getSavedStories() {
    const res = await this.http.get(this.rootUrl + '/media/savedstories?username=' + this.username)
      .toPromise();
    return this.savedStoriesList = (res as SavedStory[]);
  }

  async getUserPicture() {
    const res = await this.http.get(this.rootUrl + '/media/pictures?link=' + this.pictureLink)
      .toPromise();
    return this.fullSizePictures = (res as string[]);
  }

  async getCurrentUserInfo() {
    const res = await this.http.get(this.rootUrl + '/user/instauser?username=' + this.curUsername)
      .toPromise();
    return this.currentUser = (res as CurUser);
  }

  formatDateToLocal() {
    this.list.forEach(element => {
        element.ExpiringAt = this.datepipe.transform(element.ExpiringAt, 'dd.MM.yyyy, HH:mm:ss');
      });
  }

  formatArchiveDateToLocal() {
    this.savedStoriesList.forEach(archive => {
        archive.Stories.forEach(story => {
          story.ExpiringAt = this.datepipe.transform(story.ExpiringAt, 'dd.MM.yyyy, HH:mm:ss');
        });
      });
  }
}
