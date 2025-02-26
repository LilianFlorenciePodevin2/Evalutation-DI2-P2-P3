// src/app/password-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService, Password } from '../api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-password-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h2>Mots de passe</h2>
    <ul>
      <li *ngFor="let pwd of passwords">
        <strong>ID:</strong> {{ pwd.id }} -
        <strong>Mot de passe :</strong> {{ pwd.plainPassword }} -
        <strong>Application:</strong> {{ pwd.appName }} ({{ pwd.appType }})
        <button (click)="deletePassword(pwd.id)">Supprimer</button>
      </li>
    </ul>
  `,
  styles: [`
    ul { list-style-type: none; padding: 0; }
    li { margin-bottom: 10px; }
  `]
})
export class PasswordListComponent implements OnInit {
  passwords: Password[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadPasswords();
  }

  loadPasswords(): void {
    this.apiService.getPasswords().subscribe({
      next: (data) => {
        console.log('Données reçues:', data);
        this.passwords = data;
      },
      error: (err) => console.error(err)
    });
  }

  deletePassword(id: number): void {
    this.apiService.deletePassword(id).subscribe({
      next: () => this.loadPasswords(),
      error: (err) => console.error(err)
    });
  }
}
