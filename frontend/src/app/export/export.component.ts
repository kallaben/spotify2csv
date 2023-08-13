import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Playlist } from '../models/playlist';
import { ExportService } from '../services/export.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.css'],
})
export class ExportComponent implements OnInit {
  public readonly playlists: Observable<Playlist[]>;
  public readonly tableColumns = ['name', 'createdAt', 'creator', 'select'];
  public readonly selection = new SelectionModel<Playlist>(true, []);

  constructor(private exportService: ExportService) {
    this.playlists = exportService.getPlaylists();
  }

  ngOnInit(): void {}
}
