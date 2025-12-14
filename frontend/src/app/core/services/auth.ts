import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { LoginRequest, AuthResponse, UserInfo } from '../../features/auth/models/auth.models';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private http = inject(HttpClient);
  private router = inject(Router);

  // Cambia esta URL por la de tu backend
  private apiUrl = 'http://localhost:5140/api/auth';

  // Señal para estado de autenticación
  isAuthenticated = signal<boolean>(false);

  // BehaviorSubject para el usuario actual
  private currentUserSubject = new BehaviorSubject<UserInfo | null>(this.getUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    // Verificar si hay un token válido al iniciar
    this.checkAuthStatus();
  }

  /**
   * Realiza el login del usuario
   */
  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap((response) => {
        if (response.succes && response.token) {
          this.handleAuthSuccess(response, credentials.email);
        }
      }),
      catchError((error) => {
        console.error('Error en login:', error);
        return throwError(() => error);
      })
    );
  }

  /**
   * Maneja el éxito de la autenticación
   */
  private handleAuthSuccess(response: AuthResponse, email: string): void {
    const userInfo: UserInfo = {
      email: email,
      token: response.token,
      expiration: new Date(response.expiration),
    };

    // Guardar en localStorage
    localStorage.setItem('token', response.token);
    localStorage.setItem('userInfo', JSON.stringify(userInfo));

    // Actualizar estado
    this.isAuthenticated.set(true);
    this.currentUserSubject.next(userInfo);
  }

  /**
   * Cierra sesión del usuario
   */
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
    this.isAuthenticated.set(false);
    this.currentUserSubject.next(null);
    this.router.navigate(['/auth/login']);
  }

  /**
   * Obtiene el token actual
   */
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  /**
   * Verifica si el token ha expirado
   */
  isTokenExpired(): boolean {
    const userInfo = this.getUserFromStorage();
    if (!userInfo) return true;

    const now = new Date();
    const expiration = new Date(userInfo.expiration);
    return now >= expiration;
  }

  /**
   * Verifica el estado de autenticación
   */
  private checkAuthStatus(): void {
    const token = this.getToken();
    const isExpired = this.isTokenExpired();

    if (token && !isExpired) {
      this.isAuthenticated.set(true);
      const userInfo = this.getUserFromStorage();
      this.currentUserSubject.next(userInfo);
    } else {
      this.logout();
    }
  }

  /**
   * Obtiene la información del usuario desde localStorage
   */
  private getUserFromStorage(): UserInfo | null {
    const userInfoStr = localStorage.getItem('userInfo');
    if (!userInfoStr) return null;

    try {
      return JSON.parse(userInfoStr) as UserInfo;
    } catch {
      return null;
    }
  }
  
  /**
   * Obtiene el usuario actual
   */
  getCurrentUser(): UserInfo | null {
    return this.currentUserSubject.value;
  }
}
