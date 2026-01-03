import { Component, Input, Output, EventEmitter, inject, OnChanges, SimpleChanges } from '@angular/core';

//importaciones para formularios
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { FormInput } from '../../../ui/input/form-input/form-input';
import { Button } from '../../button-input/button';

// llamada para datos del store
import { InstallationStore } from '../../../../features/installation/installation.store';
import { InstallationDto } from '../../../../core/infrastructure/dtos/installation.dto';

@Component({
  selector: 'app-modal-installation',
  standalone: true,
  imports: [ReactiveFormsModule, FormInput, Button],
  templateUrl: './modal-installation.html',
  styleUrl: './modal-installation.css',
})
export class ModalInstallation {
  @Input() visible = false; // viene del padre
  @Output() closed = new EventEmitter<void>(); // el hijo avisa al padre
  @Input() installationToEdit: InstallationDto | null = null;

  store = inject(InstallationStore);

  installations: InstallationDto[] = [];

  constructor() { }

  //Formulario para conexión basculas
  installationForm = new FormGroup({
    name: new FormControl('', { validators: [Validators.required] }),
    address: new FormControl('', { validators: [Validators.required] }),
    location: new FormControl('', { validators: [Validators.required] }),
    city: new FormControl('', { validators: [Validators.required] }),
  });

  ngOnChanges(changes: SimpleChanges) {
    // Si llega un objeto para editar, rellenamos el formulario
    if (changes['installationToEdit'] && this.installationToEdit) {
      this.installationForm.setValue({
        name: this.installationToEdit.name,
        address: this.installationToEdit.address,
        location: this.installationToEdit.location,
        city: this.installationToEdit.city,
      });
    } else if (changes['installationToEdit'] && !this.installationToEdit) {
      // si no hay objeto, reiniciamos formulario
      this.installationForm.reset();
    }
  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.installationForm.value);
  }

  //boton guardar, creamos si es null el objeto, sino editamos ya que tenemos un objeto cargado
  sendForm() {
    if (this.installationForm.invalid) {
      this.installationForm.markAllAsTouched();
      return;
    }
    const dto: InstallationDto = {
      id: this.installationToEdit?.id,
      name: this.installationForm.value.name!,
      address: this.installationForm.value.address!,
      location: this.installationForm.value.location!,
      city: this.installationForm.value.city!,
      active: true,
    };


    if (this.installationToEdit) {
      console.log('[ModalInstallation] Guardando edición:', dto);
      this.store.updateInstallation(dto);
    } else {
      console.log('[ModalInstallation] Creando nueva instalación:', dto);
      this.store.createInstallation(dto);
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
