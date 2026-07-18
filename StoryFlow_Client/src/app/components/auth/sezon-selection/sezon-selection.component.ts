import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.getSeasons();
  }

  goToSeason(id: number) {
    this.storyService.getBySeason(id).subscribe({
      next: (response) => {
        this.router.navigateByUrl(`/sezon/${id}`);
      },
      error: (error) => this.toastr.error(error.error),
    });
  }

  getSeasons() {
    this.storyService.getSeasons().subscribe({
      next: (response) => {
        this.seasons = response;
      },
      error: (error) => console.log(error),
    });
  }
}
