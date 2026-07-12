import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StoryService } from 'src/app/_services/story.service';

@Component({
  selector: 'app-sezon-selection',
  templateUrl: './sezon-selection.component.html',
  styleUrls: ['./sezon-selection.component.scss'],
})
export class SezonSelectionComponent implements OnInit {
  seasons: any;
  constructor(
    private storyService: StoryService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.getSeasons();
  }

  goToSeason(id: number) {
    this.router.navigateByUrl(`/sezon/${id}`);
  }

  getSeasons() {
    this.storyService.getSeasons().subscribe({
      next: (response) => {
        console.log(response);
        this.seasons = response;
      },
      error: (error) => console.log(error),
    });
  }
}
