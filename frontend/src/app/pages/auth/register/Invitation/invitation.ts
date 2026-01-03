import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Auth } from '../../../../core/services/auth';

@Component({
  selector: 'app-invitation',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './invitation.html',
  styleUrls: ['./invitation.scss'],
})
export class Invitation implements OnInit {

  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private auth = inject(Auth);

  isLoading = signal(false);
  errorMessage = signal<string | null>(null);

  form!: FormGroup;
  private invitationToken!: string;

  ngOnInit(): void {
    this.invitationToken = this.route.snapshot.queryParamMap.get('token') ?? '';

    if (!this.invitationToken) {
      this.errorMessage.set('Invitación inválida o incompleta');
      return;
    }

    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const { email, password, confirmPassword } = this.form.value;

    if (password !== confirmPassword) {
      this.errorMessage.set('Las contraseñas no coinciden');
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.auth.registerWithInvitation({
      token: this.invitationToken,
      email,
      password,
    }).subscribe({
      next: () => {
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.errorMessage.set(
          err.error?.message ?? 'Error al completar el registro'
        );
        this.isLoading.set(false);
      },
    });
  }
}
