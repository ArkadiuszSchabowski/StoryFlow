import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  isModerator$: boolean = false;
  constructor(
    public authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
  ) {}

  logout() {
    this.authService.logout();
    this.toastr.success('Logged out successfully.');
    this.router.navigateByUrl('');
  }
}
