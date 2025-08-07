import { NgModule } from '@angular/core';
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
  },
  {
    path: 'genres/new',
    loadComponent: () => import('./features/genres/components/genre-form/genre-form').then(m => m.GenreFormComponent)
  },
  {
    path: 'genres/edit/:id',
    loadComponent: () => import('./features/genres/components/genre-form/genre-form').then(m => m.GenreFormComponent)
  },
  {
    path: 'authors',
    loadComponent: () => import('./features/authors/components/author-list/author-list').then(m => m.AuthorListComponent)
  },
  {
    path: 'authors/new',
    loadComponent: () => import('./features/authors/components/author-form/author-form').then(m => m.AuthorFormComponent)
  },
  {
    path: 'authors/edit/:id',
    loadComponent: () => import('./features/authors/components/author-form/author-form').then(m => m.AuthorFormComponent)
  },
  {
  path: 'books',
  loadComponent: () => import('./features/books/components/book-list/book-list').then(m => m.BookListComponent)
  },
  {
    path: 'books/new',
    loadComponent: () => import('./features/books/components/book-form/book-form').then(m => m.BookFormComponent)
  },
  {
    path: 'books/edit/:id',
    loadComponent: () => import('./features/books/components/book-form/book-form').then(m => m.BookFormComponent)
  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
