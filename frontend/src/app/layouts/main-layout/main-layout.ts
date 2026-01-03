import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { Auth } from '../../core/services/auth';
import { UserInfo } from '../../pages/auth/models/auth.models';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './main-layout.html',
  styleUrls: ['./main-layout.css'],
})
export class MainLayout {
  private authService = inject(Auth);
  currentUser: UserInfo | null = null;
  isSidebarOpen = true;
  isAdminOpen = false; // ← estado del acordeón
  
  estadosConexion = [
    { nombre: 'BASFR01', color: '#dc3545', activo: false },
    { nombre: 'BASFR02', color: '#dc3545', activo: false },
    { nombre: 'BASFR03', color: '#00921bff', activo: true },
    { nombre: 'BASFR04', color: '#6c757d', activo: false },
    { nombre: 'BASFR05', color: '#6c757d', activo: false }
  ];

  menuItems = [
    { icon: '🏠', label: 'Inicio', route: '/dashboard/inicio' },
    { icon: '📋', label: 'Pesadas', route: '/dashboard/pesadas' },
    { icon: '🔍', label: 'Consulta Pesadas', route: '/dashboard/consultas-pesadas' },
    { icon: '📊', label: 'Consulta Control', route: '/dashboard/consulta-control' },
    { icon: '📈', label: 'Gráfico', route: '/dashboard/grafico' },
    { icon: '🤖', label: 'Chat IA', route: '/dashboard/chat-ia' },
    {
      icon: '🔑',
      label: 'Admin',
      children: [
        { label: 'Usuarios', route: '/dashboard/admin/usuarios' },
        { label: 'Instalaciones', route: '/dashboard/admin/instalaciones' },
        { label: 'Básculas', route: '/dashboard/admin/basculas' },
        { label: 'Productos', route: '/dashboard/admin/productos' }
      ]
    }
  ];

  constructor(private router: Router) {}

  //login
  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
  }

  logout(): void {
    this.authService.logout();
  }
  //fin login

  toggleAdmin(): void {
    this.isAdminOpen = !this.isAdminOpen;
  }
  
  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  iniciar() {
    console.log('Iniciar clicked');
  }

  detener() {
    console.log('Detener clicked');
  }

  exportar() {
    console.log('Exportar clicked');
  }

}
