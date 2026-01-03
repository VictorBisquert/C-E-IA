import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstallationStore } from '../../../../../features/installation/installation.store';
import { InstallationDto } from '../../../../../core/infrastructure/dtos/installation.dto';

import { ModalInstallation } from '../../../modals/modal-installation/modal-installation';

@Component({
  selector: 'app-installation-data',
  standalone: true,
  imports: [CommonModule, ModalInstallation],
  templateUrl: './installation-data.html',
  styleUrls: ['./installation-data.css'],
})
export class InstallationData {
  store = inject(InstallationStore);

  isGeneralParamsModalOpen = false;
  installationEdit: InstallationDto | null = null;

  ngOnInit(): void {
    this.store.loadAll();
  }

  editInstallation(installation: InstallationDto): void {
    this.installationEdit = installation;
    this.isGeneralParamsModalOpen = true;
  }

  closeModalInstallations() {
    this.isGeneralParamsModalOpen = false;
    this.installationEdit = null;
  }

  deleteInstallation(id: string): void {
    if (!confirm('¿Seguro que deseas eliminar esta instalación?')) {
      return;
    }

    this.store.deleteInstallation(id);
  }

}
