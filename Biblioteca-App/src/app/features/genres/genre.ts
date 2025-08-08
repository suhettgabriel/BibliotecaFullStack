import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { forkJoin, map, Observable } from 'rxjs';
import { BookService } from '../../features/books/book';
export interface Genre {
  id: number;
  name: string;
  books?: any[];
  bookCount?: number;
}

export type GenreRequest = Omit<Genre, 'id'>;

@Injectable({
  providedIn: 'root'
})

export class GenreService {
  private http = inject(HttpClient);
    private bookService = inject(BookService);
  private apiUrl = `${environment.apiUrl}/genres`;

    getAllWithBookCount(): Observable<Genre[]> {
    return forkJoin({
      genres: this.http.get<Genre[]>(this.apiUrl),
      books: this.bookService.getAll()
    }).pipe(
      map(({genres, books}) => {
        return genres.map(genre => ({
          ...genre,
          bookCount: books.filter(book => book.genreId === genre.id).length
        }));
      })
    );
  }

  getAll(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.apiUrl).pipe(
      map(genres => genres.map(genre => ({
        ...genre,
        bookCount: genre.books ? genre.books.length : 0
      }))
    ));
  }

  getById(id: number): Observable<Genre> {
    return this.http.get<Genre>(`${this.apiUrl}/${id}`);
  }

  create(genre: GenreRequest): Observable<Genre> {
    return this.http.post<Genre>(this.apiUrl, genre);
  }

  update(id: number, genre: GenreRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, genre);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
