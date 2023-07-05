import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RedirectComponent } from './redirect/redirect.component';
import { HomeComponent } from './home/home.component';
import { ExportComponent } from './export/export.component';

const routes: Routes = [
  { path: 'redirect', component: RedirectComponent },
  { path: 'export', component: ExportComponent },
  { path: '', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
