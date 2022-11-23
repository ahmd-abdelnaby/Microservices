import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule }   from '../shared/shared.module';
import { IndexComponent } from './index/index.component';

import { TopSecretService }  from '../top-secret/top-secret.service';

import { TopSecretRoutingModule } from './top-secret.routing-module';
import { OrdersComponent } from './orders/orders.component';

@NgModule({
  declarations: [IndexComponent, OrdersComponent],
  providers: [ TopSecretService],
  imports: [
    CommonModule,  
    TopSecretRoutingModule,
    SharedModule
  ]
})
export class TopSecretModule { }
