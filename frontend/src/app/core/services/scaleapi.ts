import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ScaleDto } from '../infrastructure/dtos/scale.dto';
import { Scale } from '../domain/models/scale.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Scaleapi {
  private http = inject(HttpClient);
  private router = inject(Router);

  private apiUrl = 'http://localhost:5140/api/Scales';

  //GET ALL SCALES
  getScales(): Observable<Scale[]> {
    return this.http.get<Scale[]>(`${this.apiUrl}/AllScales`);
  }

  // CREATE
  create(dto: ScaleDto) {
    return this.http.post<Scale>(`${this.apiUrl}/createScale`, dto);
  }

  // GET SCALE BY ID
  getScale(id: string): Observable<ScaleDto> {
    return this.http.get<ScaleDto>(`${this.apiUrl}/${id}`);
  }

  //UPDATE SCALE
  updateScale(dto: ScaleDto) {
   return this.http.put<Scale>(`${this.apiUrl}/updateScale`, dto);
  }

  //DELETE SCALE
 deleteScale(id: string): Observable<void> {
  return this.http.delete<void>(`${this.apiUrl}/deleteScale/${id}`);
 } 


}
