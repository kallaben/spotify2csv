import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router, UrlSerializer } from '@angular/router';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css'],
})
export class RedirectComponent implements OnInit {
  redirectUrl: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serializer: UrlSerializer
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) =>
      this.handleQueryParams(params)
    );
  }

  private handleQueryParams(params: Params) {
    const stringifiedParams = new HttpParams({ fromObject: params }).toString();

    this.redirectUrl = `http://localhost:4200/api/Authentication/callback?${stringifiedParams}`;
  }
}
