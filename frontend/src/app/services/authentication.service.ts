import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private readonly baseUrl = 'api/Authentication';

  constructor(private httpClient: HttpClient) {}

  public userHasAuthenticatedWithSpotify(): Observable<boolean> {
    return this.httpClient.get<boolean>(`${this.baseUrl}/is-authenticated`);
  }
}
