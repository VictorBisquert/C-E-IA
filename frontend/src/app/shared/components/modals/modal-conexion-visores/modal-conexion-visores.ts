import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

//importaciones para formularios
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { FormInput } from '../../../ui/input/form-input/form-input';
import { Button } from '../../button-input/button';

// llamada para datos de la api
import { Scaleapi } from '../../../../core/services/scaleapi';
import { CreateScaleDto } from '../../../../core/infrastructure/dtos/scale.dto';

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

  constructor(private scaleApi: Scaleapi) {}

  //Formulario para conexión basculas
  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required] }),
    ipAddress: new FormControl('', { validators: [Validators.required] }),
    port: new FormControl<number | null>(null, { validators: Validators.required }),
  });

  sendForm() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    // Crear DTO con datos del formulario
    const dto: CreateScaleDto = {
      name: this.form.value.name!,
      ipAddress: this.form.value.ipAddress!,
      port: this.form.value.port!,
    };

    // Llamada al backend
    this.scaleApi.create(dto).subscribe({
      next: (scale) => {
        console.log('Scale creada:', scale);
        this.cerrar();
      },
      error: (err) => {
        console.error('Error al crear scale:', err);
      },
    });
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
