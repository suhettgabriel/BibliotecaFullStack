import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/auth/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'books',
    loadComponent: () => import('./features/books/components/book-list/book-list').then(m => m.BookListComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'books/new',
    loadComponent: () => import('./features/books/components/book-form/book-form').then(m => m.BookFormComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'books/edit/:id',
    loadComponent: () => import('./features/books/components/book-form/book-form').then(m => m.BookFormComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'authors',
    loadComponent: () => import('./features/authors/components/author-list/author-list').then(m => m.AuthorListComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'authors/new',
    loadComponent: () => import('./features/authors/components/author-form/author-form').then(m => m.AuthorFormComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'authors/edit/:id',
    loadComponent: () => import('./features/authors/components/author-form/author-form').then(m => m.AuthorFormComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'genres',
    loadComponent: () => import('./features/genres/components/genre-list/genre-list').then(m => m.GenreListComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'genres/new',
    loadComponent: () => import('./features/genres/components/genre-form/genre-form').then(m => m.GenreFormComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'genres/edit/:id',
    loadComponent: () => import('./features/genres/components/genre-form/genre-form').then(m => m.GenreFormComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    loadComponent: () => import('./core/auth/components/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./core/auth/components/register/register.component').then(m => m.RegisterComponent)
  },
  { path: '', redirectTo: '/books', pathMatch: 'full' },
  { path: '**', redirectTo: '/books' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
