import { Component } from '@angular/core';
import { MatCard, MatCardContent } from "@angular/material/card";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-blog-home',
  imports: [MatCard, MatCardContent, RouterLink],
  templateUrl: './blog-home.component.html',
  styleUrl: './blog-home.component.scss',
})
export class BlogHomeComponent {

}
