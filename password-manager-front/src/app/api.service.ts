// src/app/api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Password {
  id: number;
  plainPassword: string;
  applicationId: number;
  appName: string;
  appType: string;
}

export interface Application {
  id: number;
  appName: string;
  appType: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  // Mettez à jour l'URL selon votre configuration
  private apiUrl = 'https://localhost:7126/api';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'x-api-key': 'MdmyRvMafUkSlHKFOkBuUNGMmpSHujgclHsJNFPLNztG8YQyuGSIECkGA3fL2sXq3UUaU8TQVsw1zGgm2o44J0PeHReWktnZyaYWeS2zGLsJNFPLNztG8YQ8TQVsw1z' // ou gérez la clé dynamiquement
    })
  };

  constructor(private http: HttpClient) { }

  // Mots de passe
  getPasswords(): Observable<Password[]> {
    return this.http.get<Password[]>(`${this.apiUrl}/passwords`, this.httpOptions);
  }

  addPassword(passwordDto: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/passwords`, passwordDto, this.httpOptions);
  }

  deletePassword(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/passwords/${id}`, this.httpOptions);
  }

  // Applications
  getApplications(): Observable<Application[]> {
    return this.http.get<Application[]>(`${this.apiUrl}/applications`, this.httpOptions);
  }

  addApplication(appDto: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/applications`, appDto, this.httpOptions);
  }
}
