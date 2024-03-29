import { Injectable } from '@angular/core';
import { Playlist } from '../models/playlist';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  private readonly baseUrl = 'api/CsvExport';

  constructor(private httpClient: HttpClient) {}

  getPlaylists(): Observable<Playlist[]> {
    return this.httpClient.get<Playlist[]>(`${this.baseUrl}/playlists`);
  }

  getCsvForPlaylists(playlistIds: string[]): Observable<Blob> {
    const options = {
      params: new HttpParams().appendAll({ playlistIds: playlistIds }),
      responseType: 'blob' as 'json',
    };

    return this.httpClient.get<Blob>(`${this.baseUrl}/export`, options);
  }
}
