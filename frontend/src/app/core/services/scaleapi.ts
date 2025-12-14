import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CreateScaleDto } from '../infrastructure/dtos/scale.dto';
import { Scale } from '../domain/models/scale.model';

@Injectable({
  providedIn: 'root',
})
export class Scaleapi {
  private http = inject(HttpClient);
  private router = inject(Router);

  // Cambia esta URL por la de tu backend
  private apiUrl = 'http://localhost:5140/api/Scales';

   // CREATE
  create(dto: CreateScaleDto) {
    return this.http.post<Scale>(`${this.apiUrl}/createScale`, dto);
  }

}
