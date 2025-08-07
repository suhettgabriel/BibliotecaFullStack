import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { App } from './app';
import { AppRoutingModule } from './app-routing-module';
import { HeaderComponent } from './core/components/header/header';
import { JwtInterceptor } from './core/auth/interceptors/jwt.interceptor';
import { ErrorInterceptor } from './core/auth/interceptors/error.interceptor';
import { AuthService } from './core/auth/auth.service';

@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HeaderComponent
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [App]
})
export class AppModule { }
