import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

interface LoginResponse {
  token: string;
  username: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<string | null>(null);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    const username = localStorage.getItem('username');
    if (username) {
      this.currentUserSubject.next(username);
    }
  }

 login(username: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/auth/login`,
      { username, password }
    ).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('username', response.username);
        this.currentUserSubject.next(response.username);
      }),
      catchError(error => {
        let errorMessage = 'Credenciais inválidas';
        if (error.error) {
          errorMessage = error.error.message || error.error;
        }
        return throwError(() => errorMessage);
      })
    );
  }

  register(username: string, password: string): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/auth/register`,
      { username, password }
    ).pipe(
      catchError(error => {
        let errorMessage = 'Erro no registro';
        if (error.error) {
          errorMessage = error.error.message || error.error;
        }
        return throwError(() => errorMessage);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getCurrentUsername(): string | null {
    return localStorage.getItem('username');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
