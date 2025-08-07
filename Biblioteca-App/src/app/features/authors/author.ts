import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface Author {
  id: number;
  name: string;
}

export type AuthorRequest = Omit<Author, 'id'>;

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/authors`;

  getAll(): Observable<Author[]> {
    return this.http.get<Author[]>(this.apiUrl);
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
