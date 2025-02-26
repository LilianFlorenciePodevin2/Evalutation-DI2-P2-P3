// src/app/app.component.ts
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet],
  template: `
    <nav>
      <a routerLink="/passwords">Liste des mots de passe</a> |
      <a routerLink="/add-password">Ajouter un mot de passe</a> |
      <a routerLink="/applications">Applications</a>
    </nav>
    <router-outlet></router-outlet>
  `,
  styles: [`
    nav { padding: 10px; background: #efefef; }
    nav a { margin-right: 10px; text-decoration: none; }
  `]
})
export class AppComponent { }
