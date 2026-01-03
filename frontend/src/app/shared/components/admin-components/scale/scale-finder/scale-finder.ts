import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { ScaleStore, ScaleFilters } from '../../../../../features/scale/scale.store';
import { Subscription } from 'rxjs';

//modal
import { ModalConexionVisores } from '../../../modals/modal-conexion-visores/modal-conexion-visores';

@Component({
  selector: 'app-scale-finder',
  standalone: true,
  imports: [ReactiveFormsModule, ModalConexionVisores],
  templateUrl: './scale-finder.html',
  styleUrls: ['./scale-finder.css'],
})
export class ScaleFinder implements OnInit, OnDestroy {

  private store = inject(ScaleStore);
  private sub?: Subscription;

  isGeneralParamsModalOpen: boolean = false;

  form = new FormGroup({
    name: new FormControl<string>(''),
    ipAddress: new FormControl<string>(''),
    port: new FormControl<number | null>(null),
  });

  ngOnInit(): void {
  this.sub = this.form.valueChanges.subscribe(value => {
    console.log('[ScaleFinder] Form value changed:', value);
    
    const filters: ScaleFilters = {
      name: value.name ?? '',
      ipAddress: value.ipAddress ?? '',
      port: value.port ?? null
    };
    
    console.log('[ScaleFinder] Enviando filtros:', filters);
    this.store.setFilters(filters);
  });
}

  reset(): void {
    this.form.reset({
      name: '',
      ipAddress: '',
      port: null
    });

    this.store.resetFilters();
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }


    //funcionalidad para abrir y cerrar modal de conexion visores
  abrirVisores() {
    this.isGeneralParamsModalOpen = true;
    console.log(this.isGeneralParamsModalOpen);
  }
  cerrarModalVisores() {
    this.isGeneralParamsModalOpen = false;
  }

}
