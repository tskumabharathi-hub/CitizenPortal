import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { Dashboard } from './pages/dashboard/dashboard';
import { authGuard } from './core/guards/auth-guard';
import { AdminLayout } from './layouts/admin-layout/admin-layout';
import { IncidentsComponent } from './pages/incidents/incidents';

export const routes: Routes = [
    {
        path: '',
        component: LoginComponent
    },
    {
        path: '',
        component: AdminLayout,
        canActivate: [authGuard],     
        children: [
            {
                path: 'dashboard',
                component: Dashboard
            },
            {
                path: 'incidents',
                component: IncidentsComponent
            }
        ]
    },

    {
        path: '**',
        redirectTo: ''
    }

];
