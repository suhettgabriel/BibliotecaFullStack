import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { environment } from '../../../../environments/environment';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  private apiUrl = environment.apiUrl;

  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Ignora interceptação para rotas de autenticação
    if (request.url.includes(`${this.apiUrl}/auth`)) {
      return next.handle(request);
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          // Token expirado ou inválido
          this.authService.logout();
          this.router.navigate(['/login'], {
            queryParams: { returnUrl: this.router.url }
          });
        } else if (error.status === 403) {
          // Acesso não autorizado
          this.router.navigate(['/forbidden']);
        } else if (error.status === 404) {
          // Recurso não encontrado
          this.router.navigate(['/not-found']);
        }

        let errorMessage = 'Ocorreu um erro inesperado';
        if (error.error?.message) {
          errorMessage = error.error.message;
        } else if (error.message) {
          errorMessage = error.message;
        }

        console.error('HTTP Error:', error);
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
