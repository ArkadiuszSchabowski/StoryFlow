import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-moderator-panel',
  imports: [CommonModule, RouterLink],
  templateUrl: './moderator-panel.component.html',
  styleUrl: './moderator-panel.component.scss',
})
export class ModeratorPanelComponent {
  constructor(public themeService: ThemeService) {}
}
