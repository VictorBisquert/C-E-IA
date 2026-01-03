import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Scaleapi } from '../../../../core/services/scaleapi';
import { InputForm } from '../../../ui/input/input-form/input-form';

@Component({
  selector: 'app-modal-parametros-generales',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputForm
  ],
  templateUrl: './modal-parametros-generales.html',
  styleUrls: ['./modal-parametros-generales.css'],
})
export class ModalParametrosGenerales {

  private fb = inject(FormBuilder);
  private scaleApi = inject(Scaleapi);

  form: FormGroup = this.fb.group({
    name: ['', Validators.required],
    ipAddress: ['', Validators.required],
    port: [null, [Validators.required, Validators.min(1)]],
  });

  saving = false;

  guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving = true;

    this.scaleApi.create(this.form.value).subscribe({
      next: () => {
        this.saving = false;
        // aquí cerrarías el modal
      },
      error: () => {
        this.saving = false;
      }
    });
  }

  cancelar(): void {
    // cerrar modal
  }
}
