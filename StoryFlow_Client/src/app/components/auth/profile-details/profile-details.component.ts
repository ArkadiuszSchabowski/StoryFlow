import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from 'src/app/_services/auth.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { ThemeService } from 'src/app/_services/theme.service';
import { UserService } from 'src/app/_services/user.service';
import { GetUserDto } from 'src/app/models/get-user-dto';

@Component({
  selector: 'app-profile-details',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatIconModule],
  templateUrl: './profile-details.component.html',
  styleUrl: './profile-details.component.scss',
})
export class ProfileDetailsComponent implements OnInit{
  profile: GetUserDto | undefined;

  constructor(
    private userService: UserService,
    public authService: AuthService,
    public navbarService: NavbarService,
    public themeService: ThemeService
  ) {}

  ngOnInit(): void {
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
}
