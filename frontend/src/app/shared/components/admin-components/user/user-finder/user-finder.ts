import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';

import { ModalInvitationUsers } from '../../../modals/modal-invitation-users/modal-invitation-users';

@Component({
  selector: 'app-user-finder',
  standalone: true,
  imports: [ReactiveFormsModule, ModalInvitationUsers],
  templateUrl: './user-finder.html',
  styleUrl: './user-finder.css',
})
export class UserFinder {

  isGeneralParamsModalOpen: boolean = false;

  form = new FormGroup({
    name: new FormControl<string>(''),
    correo: new FormControl<string>(''),
  });

    reset(): void {
    this.form.reset({
      name: '',
      correo: ''
    });
  }


  //funcionalidad para abrir y cerrar modal de conexion visores
  OpenInvitationModal() {
    this.isGeneralParamsModalOpen = true;
    console.log(this.isGeneralParamsModalOpen);
  }
  CloseInvitationModal() {
    this.isGeneralParamsModalOpen = false;
  }

}
