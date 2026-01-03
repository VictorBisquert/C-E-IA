import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Settings } from './pages/settings/settings';

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
      loadComponent: () =>
        import('./pages/auth/login/login')
          .then(m => m.Login),
    },
    {
      path: 'register',
      children: [
        {
          path: '',
          loadComponent: () =>
            import('./pages/auth/register/register')
              .then(m => m.Register),
        },
        {
          path: 'invitation',
          loadComponent: () =>
            import('./pages/auth/register/Invitation/invitation')
              .then(m => m.Invitation),
        }
      ]
    }
  ]
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
        loadComponent: () => import('./pages/dashboard/inicio/inicio').then(m => m.Inicio)
      },
      {
        path: 'pesadas',
        loadComponent: () => import('./pages/dashboard/pesadas/pesadas').then(m => m.Pesadas)
      },
      {
        path: 'consultas-pesadas',
        loadComponent: () => import('./pages/dashboard/consulta-pesadas/consulta-pesadas').then(m => m.ConsultaPesadas)
      },
      {
        path: 'consulta-control',
        loadComponent: () => import('./pages/dashboard/consulta-control/consulta-control').then(m => m.ConsultaControl)
      },
      {
        path: 'grafico',
        loadComponent: () => import('./pages/dashboard/grafico/grafico').then(m => m.Grafico)
      },
      {
        path: 'chat-ia',
        loadComponent: () => import('./pages/dashboard/chat-ia/chat-ia').then(m => m.ChatIA)
      },
      {
        path: 'admin',
        children: [
        {
          path: '',
          redirectTo: 'usuarios',
          pathMatch: 'full',
        },
        {
          path: 'usuarios',
          loadComponent: () => import('./pages/dashboard/admin/usuarios/usuarios').then(m => m.Usuarios),
        },
        {
          path: 'basculas',
          loadComponent: () => import('./pages/dashboard/admin/basculas/basculas').then(m => m.Basculas),
        },
        {
          path: 'instalaciones',
          loadComponent: () => import('./pages/dashboard/admin/installations/installations').then(m => m.Installations),
        }]
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];
