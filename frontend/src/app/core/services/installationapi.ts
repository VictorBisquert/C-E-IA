import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { InstallationDto } from '../infrastructure/dtos/installation.dto';

@Injectable({
  providedIn: 'root',
})
export class Installationapi {
  private http = inject(HttpClient);
  private router = inject(Router);
  
  private apiUrl = 'http://localhost:5140/api/Installation';

  //GET ALL INSTALLATIONS
  getInstallations(): Observable<InstallationDto[]> {
    return this.http.get<InstallationDto[]>(`${this.apiUrl}/AllInstallations`);
  }

  //CREATE
  create(dto: InstallationDto) {
    return this.http.post<InstallationDto>(`${this.apiUrl}/createInstallation`, dto);
  }

  //GET INSTALLATION BY ID
  getInstallation(id: string): Observable<InstallationDto> {
    return this.http.get<InstallationDto>(`${this.apiUrl}/${id}`);
  }

  //UPDATE INSTALLATION
  updateInstallation(dto: InstallationDto) {
    return this.http.put<InstallationDto>(`${this.apiUrl}/updateInstallation`, dto);
  }

  //DELETE INSTALLATION
  deleteInstallation(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/deleteInstallation/${id}`);
  }

}