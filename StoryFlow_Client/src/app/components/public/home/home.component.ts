import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/_services/loader.service';
import { StoryService } from 'src/app/_services/story.service';
import { GetSentenceViewDto } from 'src/app/models/get-sentence-view-dto';
import { GetStoryViewDto } from 'src/app/models/get-story-view-dto';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit{
    constructor(
    public loader: LoaderService,
  ) {}
  ngOnInit(): void {
    this.loader.show();
  }
}
