import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
    selector: 'app-error-page',
    templateUrl: './error-page.component.html',
    styleUrls: ['./error-page.component.scss'],
    standalone: true,
    imports: [CommonModule, MatButtonModule]
})
export class ErrorPageComponent {

  constructor(private router: Router){

  }
  navigateToHome() {
    this.router.navigateByUrl('');
  }
}
