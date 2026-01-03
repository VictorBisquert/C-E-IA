import { Injectable, inject, signal } from '@angular/core';
import { Scaleapi } from '../../core/services/scaleapi';
import { ScaleDto } from '../../core/infrastructure/dtos/scale.dto';

export interface ScaleFilters {
  name: string;
  ipAddress: string;
  port: number | null;
}

@Injectable({ providedIn: 'root' })
export class ScaleStore {

  private api = inject(Scaleapi);

  // Estado
  readonly scales = signal<ScaleDto[]>([]);
  readonly filters = signal<ScaleFilters>({
    name: '',
    ipAddress: '',
    port: null
  });

  readonly loading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  // Selector derivado (mejor que signal manual)
  readonly filteredScales = signal<ScaleDto[]>([]);

  readonly selectedScale = signal<ScaleDto | null>(null);


  //Get all scales
  // Carga inicial
  loadAll(): void {
    console.log('[ScaleStore] Iniciando carga de básculas');

    this.loading.set(true);
    this.error.set(null);

    this.api.getScales().subscribe({
      next: (data) => {
        console.log('[ScaleStore] Básculas recibidas:', data);
        console.log('[ScaleStore] Total de registros:', data.length);

        this.scales.set(data);
        this.applyFilters();
      },
      error: (err) => {
        console.error('[ScaleStore] Error al cargar básculas:', err);

        this.scales.set([]);
        this.filteredScales.set([]);
        this.error.set('No se pudieron cargar las básculas');
      },
      complete: () => {
        console.log('[ScaleStore] Carga finalizada');
        this.loading.set(false);
      }
    });
  }

  //create scale
  createScale(dto: ScaleDto): void {
    this.loading.set(true);
    this.error.set(null);

    this.api.create(dto).subscribe({
      next: (created) => {
        console.log('[ScaleStore] Báscula creada:', created);

        // Opción A: recargar todo (simple y seguro)
        this.loadAll();

        // Opción B (optimizada):
        // this.scales.set([...this.scales(), created]);
        // this.applyFilters();
      },
      error: (err) => {
        console.error('[ScaleStore] Error al crear báscula', err);
        this.error.set('Error al crear la báscula');
        this.loading.set(false);
      }
    });
  }

  // GET SCALE BY ID
  loadById(id: string): void {
    if (!id) {
      this.error.set('Id de báscula inválido');
      return;
    }

    this.loading.set(true);
    this.error.set(null);
    this.selectedScale.set(null);

    this.api.getScale(id).subscribe({
      next: (scale) => {
        console.log('[ScaleStore] Báscula cargada:', scale);
        this.selectedScale.set(scale);
      },
      error: (err) => {
        console.error('[ScaleStore] Error al cargar báscula', err);
        this.error.set('No se pudo cargar la báscula');
      },
      complete: () => {
        this.loading.set(false);
      }
    });
  }

  updateScale(dto: ScaleDto): void {
    this.loading.set(true);
    this.error.set(null);

    this.api.updateScale(dto).subscribe({
      next: (updated) => {
        console.log('[ScaleStore] Báscula actualizada:', updated);

        // Estrategia segura: recargar todo
        this.loadAll();

        // Limpiamos selección
        this.selectedScale.set(null);
      },
      error: (err) => {
        console.error('[ScaleStore] Error al actualizar báscula', err);
        this.error.set('Error al actualizar la báscula');
        this.loading.set(false);
      },
      complete: () => {
        this.loading.set(false);
      }
    });
  }

  deleteScale(id: string): void {
    if (!id) {
      this.error.set('Id de báscula inválido');
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.api.deleteScale(id).subscribe({
      next: () => {
        console.log('[ScaleStore] Báscula eliminada:', id);
        this.loadAll();
      },
      error: (err) => {
        console.error('[ScaleStore] Error al eliminar báscula', err);
        this.error.set('No se pudo eliminar la báscula');
        this.loading.set(false);
      }
    });
  }


  // Actions
  setFilters(filters: ScaleFilters): void {
    console.log('[ScaleStore] setFilters llamado con:', filters);
    this.filters.set(filters);
    this.applyFilters();
  }

  resetFilters(): void {
    this.filters.set({
      name: '',
      ipAddress: '',
      port: null
    });

    this.filteredScales.set(this.scales());
  }

  // Reducer interno
  private applyFilters(): void {
    const currentFilters = this.filters();
    const allScales = this.scales();

    console.log('[ScaleStore] Aplicando filtros:', currentFilters);
    console.log('[ScaleStore] Total básculas antes de filtrar:', allScales.length);

    const { name, ipAddress, port } = currentFilters;

    const filtered = allScales.filter(scale => {
      const matchName = !name || scale.name.toLowerCase().includes(name.toLowerCase());
      const matchIp = !ipAddress || scale.ipAddress.includes(ipAddress);
      const matchPort = port === null || scale.port === port;

      return matchName && matchIp && matchPort;
    });

    console.log('[ScaleStore] Básculas después de filtrar:', filtered.length);
    this.filteredScales.set(filtered);
  }
}
