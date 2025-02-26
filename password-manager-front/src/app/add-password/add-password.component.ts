// src/app/add-password.component.ts
import { Component } from '@angular/core';
import { ApiService } from '../api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>Ajouter un mot de passe</h2>
    <form (ngSubmit)="addPassword()" #passwordForm="ngForm">
      <div>
        <label>Nom du compte:
          <input type="text" [(ngModel)]="accountName" name="accountName" required>
        </label>
      </div>
      <div>
        <label>Application:
          <select [(ngModel)]="selectedApplicationId" name="selectedApplicationId" required>
            <option *ngFor="let app of applications" [value]="app.id">{{ app.appName }}</option>
          </select>
        </label>
      </div>
      <div>
        <label>Type d'application:
          <select [(ngModel)]="selectedAppType" name="selectedAppType" required>
            <option value="Grand public">Grand public</option>
            <option value="Professionnelle">Professionnelle</option>
          </select>
        </label>
      </div>
      <div>
        <label>Mot de passe:
          <input type="text" [(ngModel)]="newPassword" name="newPassword" required>
        </label>
      </div>
      <button type="submit" [disabled]="!passwordForm.valid">Ajouter</button>
    </form>
  `,
  styles: [`
    form div { margin-bottom: 10px; }
  `]
})
export class AddPasswordComponent {
  accountName = '';
  selectedApplicationId!: number;
  selectedAppType = '';
  newPassword = '';
  applications: any[] = [];

  constructor(private apiService: ApiService) {
    // Charger dynamiquement les applications
    this.apiService.getApplications().subscribe({
      next: data => {
        this.applications = data;
        if (data.length) {
          this.selectedApplicationId = data[0].id;
          this.selectedAppType = data[0].appType;
        }
      },
      error: err => console.error(err)
    });
  }

  addPassword(): void {
    const passwordDto = {
      plainPassword: this.newPassword,
      applicationId: this.selectedApplicationId,
      appType: this.selectedAppType,
      accountName: this.accountName  // si nécessaire côté back pour identifier le compte
    };
    this.apiService.addPassword(passwordDto).subscribe({
      next: () => {
        this.newPassword = '';
        this.accountName = '';
      },
      error: err => console.error(err)
    });
  }
}
