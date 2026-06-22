import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  profile: any
  constructor(private userService: UserService){

  }

  ngOnInit(): void {
    this.userService.getProfile().subscribe({
      next: (response) => {
        console.log(response),
        this.profile = response;
      },
      error: error => console.log(error)
    });
    console.log(this.profile)
  }
}
