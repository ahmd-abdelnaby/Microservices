import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from './../shell/shell.service';
import { IndexComponent } from './index/index.component';
import { AuthGuard } from '../core/authentication/auth.guard';
import { OrdersComponent } from './orders/orders.component';

const routes: Routes = [
Shell.childRoutes([
    { path: 'topsecret', component: IndexComponent, canActivate: [AuthGuard] },       
    { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard] }       
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class TopSecretRoutingModule { }