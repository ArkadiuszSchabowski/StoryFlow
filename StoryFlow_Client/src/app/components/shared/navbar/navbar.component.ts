import { AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterLink} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { NavbarService } from 'src/app/_services/navbar.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  standalone: true,
  imports: [
    AsyncPipe,
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
    private router: Router,
    private toastr: ToastrService,
    public navbarService: NavbarService,
  ) {}

  ngOnInit(): void {
    this.authService.initializeUser();
  }

  logout() {
    this.authService.logout();
    this.navbarService.unblockButtons();
    this.toastr.success('Wylogowano pomyślnie.');
    this.router.navigateByUrl('');
  }
}
