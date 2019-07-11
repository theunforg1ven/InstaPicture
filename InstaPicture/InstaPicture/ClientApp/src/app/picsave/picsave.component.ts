import { Component, OnInit } from '@angular/core';
import { PhotoinstaService } from '../shared/photoinsta.service';

@Component({
  selector: 'app-picsave',
  templateUrl: './picsave.component.html',
  styleUrls: ['./picsave.component.scss']
})
export class PicsaveComponent implements OnInit {
  imageWidth: number;
  imageHeight: number;

  constructor(private service: PhotoinstaService) { }

  ngOnInit() {
    this.service.fullSizePictures = [];
  }

  async onGetPicture(picLink: string) {
    if (picLink != null) {
      this.service.pictureLink = picLink;
    }

    await this.service.getUserPicture().then(
      () => {
        if (this.service.fullSizePictures[0] === 'There are no media id!') {
          this.service.fullSizePictures = null;
        }
      },
      err => {
        console.log(err);
      });
  }

  downdloadImg(href: string, name: string) {
    const link = document.createElement('a');
    link.href = href;
    link.download = `${name}.jpg`;
    link.target = '_blank';
    link.style.display = 'none';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
