import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';
import { Dashboard } from './features/dashboard/dashboard';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Settings } from './features/settings/settings';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    children: [
      {
        path: 'login',
        loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
      },
      {
        path: 'register',
        loadComponent: () => import('./features/auth/register/register').then((m) => m.Register),
      },
    ],
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./layouts/main-layout/main-layout').then(m => m.MainLayout),
    canActivate: [authGuard],
    children: [
      {
      path: '',
      redirectTo: 'inicio',
      pathMatch: 'full',
      },
      {
        path: 'inicio',
        loadComponent: () => import('./features/dashboard/inicio/inicio').then(m => m.Inicio)
      },
      {
        path: 'pesadas',
        loadComponent: () => import('./features/dashboard/pesadas/pesadas').then(m => m.Pesadas)
      },
      {
        path: 'consultas-pesadas',
        loadComponent: () => import('./features/dashboard/consulta-pesadas/consulta-pesadas').then(m => m.ConsultaPesadas)
      },
      {
        path: 'consulta-control',
        loadComponent: () => import('./features/dashboard/consulta-control/consulta-control').then(m => m.ConsultaControl)
      },
      {
        path: 'grafico',
        loadComponent: () => import('./features/dashboard/grafico/grafico').then(m => m.Grafico)
      },
      {
        path: 'chat-ia',
        loadComponent: () => import('./features/dashboard/chat-ia/chat-ia').then(m => m.ChatIA)
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];
