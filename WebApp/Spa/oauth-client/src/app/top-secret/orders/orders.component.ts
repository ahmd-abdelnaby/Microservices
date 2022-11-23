import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { TopSecretService } from '../top-secret.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.sass']
})
export class OrdersComponent implements OnInit {

  data : any;
  constructor(private topSecretService:TopSecretService,private authService : AuthService) { }

  ngOnInit() {
    this.topSecretService.getOrders(this.authService.authorizationHeaderValue).subscribe(res => this.data = res)
  }

}
