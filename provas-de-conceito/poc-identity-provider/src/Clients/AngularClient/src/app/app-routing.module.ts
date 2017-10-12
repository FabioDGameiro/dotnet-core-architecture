import { MyTasksComponent } from './my-tasks/my-tasks.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { AuthGuardService } from './services/auth-guard.service';
import { ProtectedComponent } from './protected/protected.component';

const routes: Routes = [
  {
    path: '',
    children: []
  },
  {
    path: 'protected',
    component: ProtectedComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'auth-callback',
    component: AuthCallbackComponent
  },
  {
    path: 'my-tasks',
    component: MyTasksComponent,
    canActivate: [AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
