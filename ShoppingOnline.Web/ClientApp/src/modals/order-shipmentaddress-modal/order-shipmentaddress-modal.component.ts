import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-order-shipmentaddress-modal',
  templateUrl: './order-shipmentaddress-modal.component.html',
  styleUrls: ['./order-shipmentaddress-modal.component.css']
})
export class OrderShipmentaddressModalComponent  implements OnInit {

  @Input() updateSelectedOrder = new EventEmitter();
  model: any = {}
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

  updateOrderShipmentAddress() {
    //console.log(this.model.shipmentaddress)
    this.updateSelectedOrder.emit(this.model.shipmentaddress)
    this.bsModalRef.hide();
  }

}
