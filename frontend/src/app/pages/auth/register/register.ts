import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrls: ['./register.scss'],
})
export class Register {
  private fb = inject(FormBuilder);
  private authService = inject(Auth);
  private router = inject(Router);

  registerForm: FormGroup;
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  showPassword = signal(false);

  constructor() {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      companyName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  /** Alterna visibilidad de contraseña */
  togglePasswordVisibility(): void {
    this.showPassword.update((v) => !v);
  }

  /** Verifica si un campo es inválido y ha sido tocado */
  isFieldInvalid(fieldName: string): boolean {
    const field = this.registerForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  /** Mensaje de error para un campo */
  getFieldError(fieldName: string): string {
    const field = this.registerForm.get(fieldName);

    if (field?.hasError('required')) {
      switch (fieldName) {
        case 'email': return 'El email es requerido';
        case 'password': return 'La contraseña es requerida';
        case 'confirmPassword': return 'Debes confirmar la contraseña';
        case 'username': return 'El nombre de usuario es requerido';
        case 'companyName': return 'El nombre de la empresa es requerido';
      }
    }

    if (field?.hasError('email')) return 'El email no es válido';
    if (field?.hasError('minlength')) return 'La contraseña debe tener al menos 6 caracteres';

    return '';
  }

  /** Enviar formulario */
  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const { username, companyName, email, password, confirmPassword } = this.registerForm.value;

    if (password !== confirmPassword) {
      this.errorMessage.set('Las contraseñas no coinciden');
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.authService.register({ username, companyName, email, password, confirmPassword }).subscribe({
      next: (response) => {
        this.isLoading.set(false);
        if (response.succes) {
          // Redirige al dashboard o página principal
          this.router.navigate(['/dashboard']);
        } else {
          this.errorMessage.set(response.message || 'Error en el registro');
        }
      },
      error: (error) => {
        this.isLoading.set(false);
        this.errorMessage.set(error.error?.message || 'Error al registrar el usuario');
      }
    });
  }
}
