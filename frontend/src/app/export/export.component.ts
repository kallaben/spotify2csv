import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Playlist } from '../models/playlist';
import { ExportService } from '../services/export.service';
import { SelectionModel } from '@angular/cdk/collections';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.css'],
})
export class ExportComponent {
  public readonly playlistsObservable: Observable<Playlist[]>;
  public readonly tableColumns = ['name', 'createdAt', 'creator', 'select'];
  public readonly selection = new SelectionModel<Playlist>(true, []);
  private playlists?: Playlist[];

  constructor(exportService: ExportService) {
    this.playlistsObservable = exportService.getPlaylists().pipe(
      tap((playlists) => {
        this.playlists = playlists;
      }),
    );
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected(): boolean {
    if (this.playlists === null || this.playlists === undefined) {
      return false;
    }

    const numSelected = this.selection.selected.length;
    const numRows = this.playlists.length ?? 0;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows(): void {
    if (this.playlists === null || this.playlists === undefined) {
      return;
    }

    this.isAllSelected()
      ? this.selection.clear()
      : this.playlists.forEach((playlist) => this.selection.select(playlist));
  }
}
