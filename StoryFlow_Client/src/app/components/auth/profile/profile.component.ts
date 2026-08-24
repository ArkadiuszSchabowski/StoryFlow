import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { NavbarService } from 'src/app/_services/navbar.service';
import { ThemeService } from 'src/app/_services/theme.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  standalone: true,
  imports: [CommonModule, RouterLink],
})
export class ProfileComponent {
  constructor(
    public themeService: ThemeService,
    private router: Router,
    private toastr: ToastrService,
    public authService: AuthService,
    public navbarService: NavbarService,
  ) {}

  logout() {
    this.authService.logout();
    this.navbarService.unblockButtons();
    this.themeService.themeSubject.next(false);
    this.toastr.success('Wylogowano pomyślnie.');
    this.router.navigateByUrl('');
  }
}
