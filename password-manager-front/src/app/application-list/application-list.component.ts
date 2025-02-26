// src/app/application-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService, Application } from '../api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-application-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>Applications</h2>
    <ul>
      <li *ngFor="let app of applications">
        <strong>ID:</strong> {{ app.id }} -
        <strong>Nom:</strong> {{ app.appName }} -
        <strong>Type:</strong> {{ app.appType }}
      </li>
    </ul>
    <h3>Ajouter une application</h3>
    <form (ngSubmit)="addApplication()" #appForm="ngForm">
      <div>
        <label>Nom de l'application:
          <input type="text" [(ngModel)]="newAppName" name="newAppName" required>
        </label>
      </div>
      <div>
        <label>Type d'application:
          <select [(ngModel)]="newAppType" name="newAppType" required>
            <option value="Grand public">Grand public</option>
            <option value="Professionnelle">Professionnelle</option>
          </select>
        </label>
      </div>
      <button type="submit" [disabled]="!appForm.valid">Ajouter</button>
    </form>
  `,
  styles: [`
    ul { list-style-type: none; padding: 0; }
    li { margin-bottom: 8px; }
    form div { margin-bottom: 10px; }
  `]
})
export class ApplicationListComponent implements OnInit {
  applications: Application[] = [];
  newAppName = '';
  newAppType = '';

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadApplications();
  }

  loadApplications(): void {
    this.apiService.getApplications().subscribe({
      next: data => this.applications = data,
      error: err => console.error(err)
    });
  }

  addApplication(): void {
    const appDto = {
      appName: this.newAppName,
      appType: this.newAppType
    };
    this.apiService.addApplication(appDto).subscribe({
      next: () => {
        this.newAppName = '';
        this.loadApplications();
      },
      error: err => console.error(err)
    });
  }
}
