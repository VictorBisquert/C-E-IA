import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../models/auth.models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export class Login {
   private fb = inject(FormBuilder);
  private authService = inject(Auth);
  private router = inject(Router);

  loginForm: FormGroup;
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  showPassword = signal(false);

  constructor() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  /**
   * Alterna la visibilidad de la contraseña
   */
  togglePasswordVisibility(): void {
    this.showPassword.update((value) => !value);
  }

  /**
   * Verifica si un campo es inválido y ha sido tocado
   */
  isFieldInvalid(fieldName: string): boolean {
    const field = this.loginForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  /**
   * Obtiene el mensaje de error para un campo específico
   */
  getFieldError(fieldName: string): string {
    const field = this.loginForm.get(fieldName);

    if (field?.hasError('required')) {
      return `${fieldName === 'email' ? 'El email' : 'La contraseña'} es requerido`;
    }

    if (field?.hasError('email')) {
      return 'El email no es válido';
    }

    if (field?.hasError('minlength')) {
      return 'La contraseña debe tener al menos 6 caracteres';
    }

    return '';
  }

  /**
   * Maneja el envío del formulario
   */
  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    const credentials: LoginRequest = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };

    this.authService.login(credentials).subscribe({
      next: (response) => {
        this.isLoading.set(false);

        if (response.succes) {
          // Redirigir al dashboard o página principal
          this.router.navigate(['/dashboard']);
        } else {
          this.errorMessage.set(response.message || 'Error en el inicio de sesión');
        }
      },
      error: (error) => {
        this.isLoading.set(false);

        // Manejo de diferentes tipos de errores
        if (error.status === 401) {
          this.errorMessage.set('Email o contraseña incorrectos');
        } else if (error.status === 0) {
          this.errorMessage.set('No se pudo conectar con el servidor');
        } else {
          this.errorMessage.set(error.error?.message || 'Error al iniciar sesión');
        }
      },
    });
  }
}
