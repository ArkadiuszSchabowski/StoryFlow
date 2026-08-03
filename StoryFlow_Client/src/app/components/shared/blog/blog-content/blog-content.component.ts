import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BlogService } from 'src/app/_services/blog.service';

@Component({
  selector: 'app-blog-content',
  imports: [RouterLink, DatePipe],
  templateUrl: './blog-content.component.html',
  styleUrl: './blog-content.component.scss',
})
export class BlogContentComponent implements OnInit {
  slug: string | null = null;
  blogPost: any;

  constructor(
    private route: ActivatedRoute,
    private blogService: BlogService,
    private meta: Meta,
    private title: Title,
  ) {}

  ngOnInit(): void {
    this.getSlugFromRoute();
  }

  setMetaTitleAndMetaDescription() {
    if (!this.blogPost) return;

    this.title.setTitle(this.blogPost.metaTitleContent);
    this.meta.updateTag({
      name: 'description',
      content: this.blogPost.metaTitleDescription,
    });

    this.meta.updateTag({
      property: 'og:title',
      content: this.blogPost.metaTitleContent,
    });
    this.meta.updateTag({
      property: 'og:description',
      content: this.blogPost.metaTitleDescription,
    });
    this.meta.updateTag({
      property: 'og:url',
      content: `https://www.storyflow.pl/blog/${this.blogPost.slug}`,
    });

    if (this.blogPost.imageUrl) {
      this.meta.updateTag({
        property: 'og:image',
        content: this.blogPost.imageUrl,
      });
    }
  }

  getSlugFromRoute(): void {
    this.slug = this.route.snapshot.paramMap.get('slug');

    if (this.slug != null) {
      this.getBySlug(this.slug);
    }
  }

  getBySlug(slug: string) {
    this.blogService.getBySlug(slug).subscribe({
      next: (response) => {
        this.blogPost = response;
        console.log(this.blogPost);
        this.setMetaTitleAndMetaDescription();
      },
      error: (error) => console.log(error),
    });
  }
}
