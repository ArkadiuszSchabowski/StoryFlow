import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink} from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  standalone: true,
  imports: [
    AsyncPipe,
    CommonModule,
    MatButtonModule,
    MatToolbarModule,
    MatMenuModule,
    MatIconModule,
    RouterLink
  ],
})
export class NavbarComponent implements OnInit {
  isModerator$: boolean = false;

  constructor(
    public authService: AuthService,
    public navbarService: NavbarService,
    public themeService: ThemeService
  ) {}

  ngOnInit(): void {
    this.authService.initializeUser();
  }
}
