import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScaleStore } from '../../../../../features/scale/scale.store';
import { ScaleDto } from '../../../../../core/infrastructure/dtos/scale.dto';
import { ModalConexionVisores } from '../../../modals/modal-conexion-visores/modal-conexion-visores';

@Component({
  selector: 'app-scale-data',
  standalone: true,
  imports: [CommonModule, ModalConexionVisores], // ← ESTO ES CRÍTICO
  templateUrl: './scale-data.html',
  styleUrls: ['./scale-data.css'],

})
export class ScaleData {
  store = inject(ScaleStore);

  isGeneralParamsModalOpen = false;
  scaleToEdit: ScaleDto | null = null;

  ngOnInit(): void {
    this.store.loadAll();
  }

  editScale(scale: ScaleDto): void {
    console.log('[ScaleData] Pulsado editar para:', scale);

    this.scaleToEdit = scale; // enviamos el objeto al modal
    this.isGeneralParamsModalOpen = true;
  }

  cerrarModalVisores() {
    this.isGeneralParamsModalOpen = false;
    this.scaleToEdit = null;
  }

  deleteScale(id: string): void {
  if (!confirm('¿Seguro que deseas eliminar esta báscula?')) {
    return;
  }

  this.store.deleteScale(id);
}

}