import { Injectable } from '@angular/core';
import { Playlist } from '../models/playlist';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  constructor() {}

  getPlaylists(): Observable<Playlist[]> {
    const playlists: Playlist[] = [
      { createdAt: new Date(), creator: 'Ben', name: 'Favorites', id: '1' },
      { createdAt: new Date(), creator: 'Ben', name: 'Oldschool', id: '2' },
      { createdAt: new Date(), creator: 'Ben', name: 'Run', id: '3' },
    ];

    return of(playlists);
  }

  getCsvForPlaylists(playlistIds: string[]): Observable<boolean> {
    console.log({ playlistIds });

    return of(true);
  }
}
