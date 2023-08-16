import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  public isAuthenticated: Observable<boolean>;
  public readonly window = window;

  constructor(private authenticationService: AuthenticationService) {
    this.isAuthenticated =
      this.authenticationService.userHasAuthenticatedWithSpotify();
  }
}
