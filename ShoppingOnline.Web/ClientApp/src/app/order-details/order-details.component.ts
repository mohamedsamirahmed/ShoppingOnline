import { Component, Input, OnInit } from '@angular/core';
import { OrderItem } from '../../models/order-item';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  @Input() order: OrderItem;

  constructor() { }

  ngOnInit() {
  }

}
