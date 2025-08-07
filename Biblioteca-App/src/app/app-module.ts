import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HeaderComponent } from './core/components/header/header';
import { GenreListComponent } from './features/genres/components/genre-list/genre-list';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'genres',
    pathMatch: 'full'
  },
  {
    path: 'genres',
    loadComponent: () => import('./features/genres/components/genre-list/genre-list').then(m => m.GenreListComponent)
  }
];
@NgModule({
  declarations: [
    App,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    GenreListComponent,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [App]
})
export class AppModule { }
