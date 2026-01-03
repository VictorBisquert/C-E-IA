import { Component, Input, Output, EventEmitter, inject, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

//importaciones para formularios
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { FormInput } from '../../../ui/input/form-input/form-input';
import { Button } from '../../button-input/button';

// llamada para datos del store
import { ScaleDto } from '../../../../core/infrastructure/dtos/scale.dto';
import { ScaleStore } from '../../../../features/scale/scale.store';

@Component({
  selector: 'app-modal-conexion-visores',
  standalone: true,
  imports: [ReactiveFormsModule, FormInput, Button],
  templateUrl: './modal-conexion-visores.html',
  styleUrls: ['./modal-conexion-visores.css'],
})
export class ModalConexionVisores {
  @Input() visible = false; // viene del padre
  @Output() closed = new EventEmitter<void>(); // el hijo avisa al padre
  @Input() scaleToEdit: ScaleDto | null = null; // <-- input del objeto

  store = inject(ScaleStore);

  scales: ScaleDto[] = [];


  constructor() {}

  //Formulario para conexión basculas
  scaleForm = new FormGroup({
    name: new FormControl('', { validators: [Validators.required] }),
    ipAddress: new FormControl('', { validators: [Validators.required] }),
    port: new FormControl<number | null>(null, { validators: Validators.required }),
  });

    ngOnChanges(changes: SimpleChanges) {
    // Si llega un objeto para editar, rellenamos el formulario
    if (changes['scaleToEdit'] && this.scaleToEdit) {
      this.scaleForm.setValue({
        name: this.scaleToEdit.name,
        ipAddress: this.scaleToEdit.ipAddress,
        port: this.scaleToEdit.port,
      });
    } else if (changes['scaleToEdit'] && !this.scaleToEdit) {
      // si no hay objeto, reiniciamos formulario
      this.scaleForm.reset();
    }
  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.scaleForm.value);
  }

  //boton guardar, creamos si es null el objeto, sino editamos ya que tenemos un objeto cargado
  sendForm() {
  if (this.scaleForm.invalid) {
    this.scaleForm.markAllAsTouched();
    return;
  }

  const dto: ScaleDto = {
    id: this.scaleToEdit?.id, 
    name: this.scaleForm.value.name!,
    ipAddress: this.scaleForm.value.ipAddress!,
    port: this.scaleForm.value.port!,
    isActive: true,
  };

  if (this.scaleToEdit) {
    console.log('[ModalConexionVisores] Guardando edición:', dto);
    this.store.updateScale(dto);
  } else {
    console.log('[ModalConexionVisores] Creando nueva báscula:', dto);
    this.store.createScale(dto);
  }

  this.cerrar();
}
  //fin formulario

  cerrar() {
    this.closed.emit();
  }

  onBackdropClick(event: MouseEvent) {
    if ((event.target as HTMLElement).classList.contains('modal-backdrop')) {
      this.cerrar();
    }
  }
}
