import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatCard, MatCardContent } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { LottieComponent } from 'ngx-lottie';
import { BlogService } from 'src/app/_services/blog.service';
import { LoaderService } from 'src/app/_services/loader.service';
import { ThemeService } from 'src/app/_services/theme.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-blog-home',
  imports: [CommonModule, MatCard, MatCardContent, RouterLink, DatePipe, LottieComponent],
  templateUrl: './blog-home.component.html',
  styleUrl: './blog-home.component.scss',
})
export class BlogHomeComponent implements OnInit {
  blogPosts: any;
  apiUrl = environment.apiUrl;

  constructor(
    private blogService: BlogService,
    public themeService: ThemeService,
    public loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.getVisibleBlogPosts();
  }

  getVisibleBlogPosts() {
    this.blogService.getVisibleBlogPosts().subscribe({
      next: (response) => {
        this.blogPosts = response;
        console.log(this.blogPosts);
      },

      error: (error) => console.log(error),
    });
  }
}
