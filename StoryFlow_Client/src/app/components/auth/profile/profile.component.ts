import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { GetUserDto } from 'src/app/models/get-user-dto';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss'],
    standalone: false
})
export class ProfileComponent implements OnInit {
  profile: GetUserDto | undefined;

  constructor(private userService: UserService) {}

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
