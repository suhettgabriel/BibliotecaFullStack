import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { map, Observable } from 'rxjs';

export interface Author {
  id: number;
  name: string;
  books?: any[];
  bookCount?: number;
}

export type AuthorRequest = Omit<Author, 'id'>;

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/authors`;

// src/app/features/authors/author.ts (serviço)
getAll(): Observable<Author[]> {
  return this.http.get<Author[]>(this.apiUrl).pipe(
    map(authors => authors.map(author => ({
      ...author,
      bookCount: author.books?.length || 0
    })))
  );
}

  getById(id: number): Observable<Author> {
    return this.http.get<Author>(`${this.apiUrl}/${id}`);
  }

  create(author: AuthorRequest): Observable<Author> {
    return this.http.post<Author>(this.apiUrl, author);
  }

  update(id: number, author: AuthorRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, author);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
