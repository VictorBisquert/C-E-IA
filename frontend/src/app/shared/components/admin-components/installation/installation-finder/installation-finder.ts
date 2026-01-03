import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { InstallationStore, InstallationFilters } from '../../../../../features/installation/installation.store';
import { Subscription } from 'rxjs';

//modal
import { ModalInstallation } from '../../../modals/modal-installation/modal-installation';

@Component({
  selector: 'app-installation-finder',
  standalone: true,
  imports: [ReactiveFormsModule, ModalInstallation],
  templateUrl: './installation-finder.html',
  styleUrls: ['./installation-finder.css'],
})
export class InstallationFinder implements OnInit, OnDestroy {

  private store = inject(InstallationStore);
  private sub?: Subscription;

  isGeneralParamsModalOpen: boolean = false;

  form = new FormGroup({
    name: new FormControl<string>(''),
    address: new FormControl<string>(''),
    location: new FormControl<string>(''),
    city: new FormControl<string>(''),
  });

  ngOnInit(): void {
    this.sub = this.form.valueChanges.subscribe(value => {
      console.log('[InstallationFinder] Form value changed:', value);

      const filters: InstallationFilters = {
        name: value.name ?? '',
        address: value.address ?? '',
        location: value.location ?? '',
        city: value.city ?? ''
      };

      console.log('[InstallationFinder] Enviando filtros:', filters);
      this.store.setFilters(filters);
    });
  }

  reset(): void {
    this.form.reset({
      name: '',
      address: '',
      location: '',
      city: ''
    });

    this.store.resetFilters();
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  //funcionalidad para abrir y cerrar modal de conexion visores
  openModalInstallation() {
    this.isGeneralParamsModalOpen = true;
    console.log(this.isGeneralParamsModalOpen);
  }
  closeModalInstallation() {
    this.isGeneralParamsModalOpen = false;
  }

}
