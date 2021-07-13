import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment';
import { Lookup } from '../../models/lookup';
import { Order } from '../../models/order';
import { LookupService } from '../../Services/lookup-service';

@Component({
  selector: 'app-order-delivery-modals',
  templateUrl: './order-delivery-modals.component.html',
  styleUrls: ['./order-delivery-modals.component.css']
})
export class OrderDeliveryModalsComponent implements OnInit {

  @Input() updateSelectedOrder = new EventEmitter();
  order: Order;
  public selectedStatus: any;

  public _allOrderStatus: Lookup[];
  serviceBaseUrl = environment.apiEndpoint;
  orderStatusServiceEndpoint = this.serviceBaseUrl + 'admin/GetOrderStatus';

  constructor(public bsModalRef: BsModalRef, private lookupService: LookupService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.loadOrderStatusLookup();
  }

  loadOrderStatusLookup() {
    this.lookupService.getlookupList(this.orderStatusServiceEndpoint).subscribe((response: any) => {
      if (response.returnStatus)
        this._allOrderStatus = response.entity;
    }, error => {
      console.log(error);
      this.toastrService.error(error.message);
    });
  }

  updateOrderStatus() {
    this.updateSelectedOrder.emit(this.selectedStatus)
    this.bsModalRef.hide();
  }

}
