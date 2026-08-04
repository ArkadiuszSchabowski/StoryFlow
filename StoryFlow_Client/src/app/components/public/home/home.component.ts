import { AsyncPipe, CommonModule} from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { LottieComponent } from 'ngx-lottie';
import { LoaderService } from 'src/app/_services/loader.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { StoryService } from 'src/app/_services/story.service';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [AsyncPipe, CommonModule, LottieComponent, MatButtonModule, RouterLink],
})
export class HomeComponent implements OnInit {
  constructor(
    private storyService: StoryService,
    private router: Router,
    public loaderService: LoaderService,
    private navbarService: NavbarService,
    public themeService: ThemeService
  ) {}

  ngOnInit(): void {}

  navigateToWelcomeStory() {
    const WELCOME_STORY_ID: number = 79;

    this.navbarService.blockButtons();
    this.loaderService.show();

    const start = Date.now();
    const minTime = 2000;

    this.storyService.getWelcomeStory().subscribe({
      next: () => {
        const elapsedTime = Date.now() - start;
        const remaining = Math.max(0, minTime - elapsedTime);

        setTimeout(() => {
          this.router.navigateByUrl(`text/${WELCOME_STORY_ID}`);
          this.loaderService.hide();
          this.navbarService.unblockButtons();
        }, remaining);
      },
      error: () => {
        this.loaderService.hide();
        this.navbarService.unblockButtons();
      },
    });
  }
}
