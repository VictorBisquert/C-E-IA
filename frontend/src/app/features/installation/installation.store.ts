import { Injectable, inject, signal } from '@angular/core';
import { Installationapi } from '../../core/services/installationapi';
import { InstallationDto } from '../../core/infrastructure/dtos/installation.dto';

export interface InstallationFilters {
    name: string;
    address: string;
    location: string;
    city: string;
}

@Injectable({ providedIn: 'root' })
export class InstallationStore {

    private api = inject(Installationapi);

    readonly installations = signal<InstallationDto[]>([]);
    readonly filters = signal<InstallationFilters>({
        name: '',
        address: '',
        location: '',
        city: ''
    });

    readonly loading = signal<boolean>(false);
    readonly error = signal<string | null>(null);

    readonly filteredInstallations = signal<InstallationDto[]>([]);

    readonly selectedInstallation = signal<InstallationDto | null>(null);

    //Get all installations
    // Carga inicial
    loadAll(): void {
        console.log('[InstallationStore] Iniciando carga de instalaciones');

        this.loading.set(true);
        this.error.set(null);

        this.api.getInstallations().subscribe({
            next: (data) => {
                console.log('[InstallationStore] Básculas recibidas:', data);
                console.log('[InstallationStore] Total de registros:', data.length);

                this.installations.set(data);
                this.applyFilters();
            },
            error: (err) => {
                console.error('[InstallationStore] Error al cargar instalaciones:', err);

                this.installations.set([]);
                this.filteredInstallations.set([]);
                this.error.set('No se pudieron cargar las instalaciones');
            },
            complete: () => {
                console.log('[InstallationStore] Carga finalizada');
                this.loading.set(false);
            }
        });
    }

    //create installation
    createInstallation(dto: InstallationDto): void {
      this.loading.set(true);
      this.error.set(null);
    
      this.api.create(dto).subscribe({
        next: (created) => {
          console.log('[InstallationStore] Instalación creada:', created);
          this.loadAll();
        },
        error: (err) => {
          console.error('[InstallationStore] Error al crear instalación', err);
          this.error.set('Error al crear la instalación');
          this.loading.set(false);
        }
      });
    }

    // GET SCALE BY ID
    loadById(id: string): void {
        if (!id) {
            this.error.set('Id de instalación inválido');
            return;
        }

        this.loading.set(true);
        this.error.set(null);
        this.selectedInstallation.set(null);

        this.api.getInstallation(id).subscribe({
            next: (installation) => {
                console.log('[InstallationStore] instalación cargada:', installation);
                this.selectedInstallation.set(installation);
            },
            error: (err) => {
                console.error('[InstallationStore] Error al cargar instalación', err);
                this.error.set('No se pudo cargar la instalación');
            },
            complete: () => {
                this.loading.set(false);
            }
        });
    }

    //UPDATE INSTALLATION
    updateInstallation(dto: InstallationDto): void {
        this.loading.set(true);
        this.error.set(null);
    
        this.api.updateInstallation(dto).subscribe({
          next: (updated) => {
            console.log('[InstallationStore] instalación actualizada:', updated);
            this.loadAll();

            this.selectedInstallation.set(null);
          },
          error: (err) => {
            console.error('[InstallationStore] Error al actualizar instalación', err);
            this.error.set('Error al actualizar la instalación');
            this.loading.set(false);
          },
          complete: () => {
            this.loading.set(false);
          }
        });
      }

    //DELETE INSTALLATION
    deleteInstallation(id: string): void {
        if (!id) {
            this.error.set('Id de instalación inválido');
            return;
        }

        this.loading.set(true);
        this.error.set(null);

        this.api.deleteInstallation(id).subscribe({
            next: () => {
                console.log('[InstallationStore] instalación eliminada:', id);
                this.loadAll();
            },
            error: (err) => {
                console.error('[InstallationStore] Error al eliminar instalación', err);
                this.error.set('No se pudo eliminar la instalación');
                this.loading.set(false);
            }
        });
    }

    // Actions
    setFilters(filters: InstallationFilters): void {
        console.log('[InstallationStore] setFilters llamado con:', filters);
        this.filters.set(filters);
        this.applyFilters();
    }

    resetFilters(): void {
        this.filters.set({
            name: '',
            address: '',
            location: '',
            city: ''
        });

        this.filteredInstallations.set(this.installations());
    }

      // Reducer interno
  private applyFilters(): void {
    const currentFilters = this.filters();
    const allInstallations = this.installations();

    console.log('[InstallationStore] Aplicando filtros:', currentFilters);
    console.log('[InstallationStore] Total instalaciones antes de filtrar:', allInstallations.length);

    const { name, address, location, city } = currentFilters;

    const filtered = allInstallations.filter(installation => {
      const matchName = !name || installation.name.toLowerCase().includes(name.toLowerCase());
      const matchAddress = !address || installation.address.toLowerCase().includes(address.toLowerCase());
      const matchLocation = !location || installation.location.toLowerCase().includes(location.toLowerCase());
      const matchCity = !city || installation.city.toLowerCase().includes(city.toLowerCase());

      return matchName && matchAddress && matchLocation && matchCity;
    });

    console.log('[InstallationStore] Instalaciones después de filtrar:', filtered.length);
    this.filteredInstallations.set(filtered);
  }
}
