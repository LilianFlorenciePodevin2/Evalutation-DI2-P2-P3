// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { PasswordListComponent } from './password-list/password-list.component';
import { AddPasswordComponent } from './add-password/add-password.component';
import { ApplicationListComponent } from './application-list/application-list.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: 'passwords', pathMatch: 'full' },
  { path: 'passwords', component: PasswordListComponent },
  { path: 'add-password', component: AddPasswordComponent },
  { path: 'applications', component: ApplicationListComponent },
  { path: '**', redirectTo: '' }
];
