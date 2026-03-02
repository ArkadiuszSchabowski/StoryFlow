import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService){

  }

  logout(){
    this.authService.logout();
    this.toastr.success("Logged out successfully.")
    this.router.navigateByUrl('');
  }
}
