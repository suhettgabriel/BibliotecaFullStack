import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Author } from '../authors/author';
import { Genre } from '../genres/genre';

export interface Book {
  id: number;
  title: string;
  year: number;
  authorId: number;
  genreId: number;
  author?: Author;
  genre?: Genre;
}

export interface BookRequest {
  title: string;
  year: number;
  authorId: number;
  genreId: number;
}

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/books`;

  getAll(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }

  getById(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`);
  }

  create(book: BookRequest): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book);
  }

  update(id: number, book: BookRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, book);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
