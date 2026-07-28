import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StoryService } from 'src/app/_services/story.service';
import { UserService } from 'src/app/_services/user.service';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
import { GetUserDto } from 'src/app/models/get-user-dto';

@Component({
  selector: 'app-sezon-selection',
  templateUrl: './sezon-selection.component.html',
  styleUrls: ['./sezon-selection.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule
  ],
})
export class SezonSelectionComponent implements OnInit {
  seasons: any;
  stories: GetStoryViewDto[] = [];
  profile: GetUserDto | undefined;

  constructor(
    private storyService: StoryService,
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
  ) {}

  ngOnInit(): void {
    this.getSeasons();
    this.getProfile();
  }

  getProfile() {
    this.userService.getProfile().subscribe({
      next: (response) => {
        this.profile = response;
      },
      error: () => {},
    });
  }

  goToSeason(id: number) {
    this.storyService.getBySeason(id).subscribe({
      next: () => {
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
