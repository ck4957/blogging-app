import { Component, OnInit, ViewChild } from '@angular/core';
import { BlogImage } from '../models/blog-image.model';
import { Observable } from 'rxjs';
import { ImageService } from './image.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent implements OnInit {
  private file?: File;
  fileName: string = '';
  title: string = '';
  images$?: Observable<BlogImage[]>;

  @ViewChild('form', { static: false}) imageUploadForm?: NgForm;
  
  constructor(private imageService: ImageService) {}

  ngOnInit(): void {
    this.getImages();
  }

  

  selectImage(image: BlogImage) {
    this.imageService.selectImage(image);
  }
  uploadImage() {
    if (this.file && this.fileName !== '' && this.title !== '') {
      // Image service to upload the image
      this.imageService.uploadImage(this.file, this.fileName, this.title)
      .subscribe({
        next: (response) => {
          this.imageUploadForm?.resetForm();
          this.getImages();
        }
      });
    }
  }
  onFileUploadChange(event: Event) {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  private getImages() {
    this.images$ = this.imageService.getAllImages();
  }
}

