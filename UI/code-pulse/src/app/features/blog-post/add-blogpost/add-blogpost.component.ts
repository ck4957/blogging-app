import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogPost } from '../models/blog-post.model';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Category } from '../../category/models/category.model';
import { BlogPostService } from '../services/blog-post.service';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit, OnDestroy {

  model: AddBlogPost;
  categories$?: Observable<Category[]>;
  isImageSelectorVisible: boolean = false;

  constructor(private blogPostService: BlogPostService,
    private router: Router,
    private categoryService: CategoryService
  ) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      author: '',
      publishedDate: new Date(),
      isVisible: true,
      categories: []
    }
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  openImageSelector() {
    throw new Error('Method not implemented.');
  }

  closeImageSelector() {
    throw new Error('Method not implemented.');
  }

  onFormSubmit() {
    this.blogPostService.createBlogPost(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts');
      }
    });
  }

  ngOnDestroy(): void {

  }

}
