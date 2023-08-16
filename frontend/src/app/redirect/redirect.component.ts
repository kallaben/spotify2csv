import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css'],
})
export class RedirectComponent implements OnInit {
  redirectUrl: string = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) =>
      this.handleQueryParams(params),
    );
  }

  private handleQueryParams(params: Params) {
    const stringifiedParams = new HttpParams({ fromObject: params }).toString();

    this.redirectUrl = `${window.location.origin}/api/Authentication/callback?${stringifiedParams}`;
  }
}
