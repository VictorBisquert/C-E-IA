import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

//modal
import { ModalConexionVisores } from '../../../shared/components/modals/modal-conexion-visores/modal-conexion-visores';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [CommonModule, ModalConexionVisores],
  templateUrl: './inicio.html',
  styleUrls: ['./inicio.css'],
})
export class Inicio {
  isSidebarOpen = true;

  //abrir y cerrar modal
  isGeneralParamsModalOpen: boolean = false;

  estadosConexion = [
    { nombre: 'BASFR01', color: '#dc3545', activo: false },
    { nombre: 'BASFR02', color: '#dc3545', activo: false },
    { nombre: 'BASFR03', color: '#00921bff', activo: true },
    { nombre: 'BASFR04', color: '#6c757d', activo: false },
    { nombre: 'BASFR05', color: '#6c757d', activo: false }
  ];

  iniciar() {
    console.log('Iniciar clicked');
  }

  detener() {
    console.log('Detener clicked');
  }

  exportar() {
    console.log('Exportar clicked');
  }

  //funcionalidad para abrir y cerrar modal de conexion visores
  abrirVisores() {
    this.isGeneralParamsModalOpen = true;
    console.log(this.isGeneralParamsModalOpen);
  }
  cerrarModalVisores() {
    this.isGeneralParamsModalOpen = false;
  }
  
  insertarManual(){

  }
}
