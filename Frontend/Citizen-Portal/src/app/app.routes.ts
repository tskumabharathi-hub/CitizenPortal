import { Routes } from '@angular/router';
import { Signup } from './signup/signup';
import { Login } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { ReportIssue } from './report-issue/report-issue';
import { Home } from './home/home';
import { Profile } from './profile/profile';
import { ViewIncident } from './view-incident/view-incident';

export const routes: Routes = [
  { path: '', redirectTo: 'signup', pathMatch: 'full' },
  { path: 'signup', component: Signup },
  { path: 'login', component: Login },
  { 
    path: 'dashboard', 
    component: Dashboard,
    children:[
      { path: '', component: Home },
      { path: 'report-issue', component: ReportIssue },
      { path:'profile', component: Profile},
      { path:'view-incident',component:ViewIncident}
    ]
  }
];
