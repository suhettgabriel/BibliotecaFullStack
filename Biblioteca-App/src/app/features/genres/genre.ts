import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface Genre {
  id: number;
  name: string;
}

export type GenreRequest = Omit<Genre, 'id'>;

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/genres`;

  getAll(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.apiUrl);
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
