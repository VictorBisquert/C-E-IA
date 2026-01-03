import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { 
    ControlContainer, 
    FormGroupDirective, 
    ReactiveFormsModule,
    FormControl, 
    AbstractControl // <-- Puedes usarlo, pero es mejor tipar como FormControl si es lo que usas en el template
} from '@angular/forms';

@Component({
  selector: 'app-form-input',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './form-input.html',
  styleUrls: ['./form-input.css'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }],
})
export class FormInput {
  @Input() label!: string;
  @Input() name!: string;
  @Input() type: string = 'text';
  @Input() placeholder: string = '';
  @Input() readOnly: boolean = false;
  @Input() required: boolean = false;

  constructor(private ctrlContainer: ControlContainer) {}

  get control() {
    return this.ctrlContainer.control?.get(this.name) as FormControl | null;
  }

  getErrorMessage(): string {
    if (!this.control || !this.control.errors) return '';
    if (this.control.errors['required']) return 'Este campo es obligatorio';
    if (this.control.errors['email']) return 'Formato de email no válido';
    if (this.control.errors['minlength'])
      return `Mínimo ${this.control.errors['minlength'].requiredLength} caracteres`;
    if (this.control.errors['maxlength'])
      return `Máximo ${this.control.errors['maxlength'].requiredLength} caracteres`;
    return 'Campo inválido';
  }
}
