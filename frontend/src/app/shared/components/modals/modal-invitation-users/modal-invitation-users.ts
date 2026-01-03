import { Component, Input, Output, EventEmitter, inject, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { Invitation } from '../../../../core/domain/models/invitation.model';

import { Auth } from '../../../../core/services/auth';

@Component({
  selector: 'app-modal-invitation-users',
  imports: [ReactiveFormsModule],
  templateUrl: './modal-invitation-users.html',
  styleUrl: './modal-invitation-users.css',
})
export class ModalInvitationUsers {
  @Input() visible = false; // viene del padre
  @Output() closed = new EventEmitter<void>(); // el hijo avisa al padre
  @Output() invitationSent = new EventEmitter<void>(); // 👈 opcional

  private invitationApi = inject(Auth);

  constructor() {}

    //Formulario para conexión basculas
  InviteForm = new FormGroup({
    email: new FormControl('', {
      validators: [Validators.required, Validators.email],
      nonNullable: true,
    }),
    role: new FormControl('', {
      validators: [Validators.required],
      nonNullable: true,
    }),
  });

  loading = false;
  errorMessage: string | null = null;

  sendForm() {
    if (this.InviteForm.invalid) return;

    this.loading = true;
    this.errorMessage = null;

    this.invitationApi.createInvitation(this.InviteForm.getRawValue())
      .subscribe({
        next: () => {
          this.loading = false;
          this.InviteForm.reset();
          this.invitationSent.emit(); // para refrescar lista si quieres
          this.cerrar();
        },
        error: (err) => {
          this.loading = false;
          this.errorMessage = err?.error?.message ?? 'Error al enviar la invitación';
        }
      });
  }

  cerrar() {
    this.closed.emit();
  }

  onBackdropClick(event: MouseEvent) {
    if ((event.target as HTMLElement).classList.contains('modal-backdrop')) {
      this.cerrar();
    }
  }
  
}
