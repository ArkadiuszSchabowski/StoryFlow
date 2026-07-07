import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { NavbarService } from 'src/app/_services/navbar.service';

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
    public navbarService: NavbarService,
  ) {}

  logout() {
    this.authService.logout();
    this.navbarService.unblockButtons();
    this.toastr.success('Wylogowano pomyślnie.');
    this.router.navigateByUrl('');
  }
}
