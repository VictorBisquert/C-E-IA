import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { Auth } from '../services/auth';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router);

  if (authService.isAuthenticated() && !authService.isTokenExpired()) {
    return true;
  }

  // Guardar la URL a la que intentaba acceder
  router.navigate(['/auth/login'], {
    queryParams: { returnUrl: state.url },
  });

  return false;
};
