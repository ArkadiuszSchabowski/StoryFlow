import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-administrator-panel',
  imports: [CommonModule, RouterLink],
  templateUrl: './administrator-panel.component.html',
  styleUrl: './administrator-panel.component.scss',
})
export class AdministratorPanelComponent {
constructor(public themeService: ThemeService){}
}
