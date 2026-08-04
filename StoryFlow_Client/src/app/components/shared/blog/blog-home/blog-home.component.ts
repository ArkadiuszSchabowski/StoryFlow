import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatCard, MatCardContent } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { BlogService } from 'src/app/_services/blog.service';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-blog-home',
  imports: [CommonModule, MatCard, MatCardContent, RouterLink, DatePipe],
  templateUrl: './blog-home.component.html',
  styleUrl: './blog-home.component.scss',
})
export class BlogHomeComponent implements OnInit {
  blogPosts: any;
  constructor(
    private blogService: BlogService,
    public themeService: ThemeService,
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.blogService.getAll().subscribe({
      next: (response) => {
        this.blogPosts = response;
        console.log(this.blogPosts);
      },

      error: (error) => console.log(error),
    });
  }
}
