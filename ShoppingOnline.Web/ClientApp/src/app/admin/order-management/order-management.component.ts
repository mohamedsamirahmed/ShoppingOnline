import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { OrderDeliveryModalsComponent } from '../../../modals/order-delivery-modals/order-delivery-modals.component';
import { Order } from '../../../models/order';
import { OrderStatus } from '../../../models/order-status';
import { Pagination } from '../../../models/Pagination';
import { AdminService } from '../../../Services/admin.service';
import { LookupService } from '../../../Services/lookup-service';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css']
})
export class OrderManagementComponent implements OnInit {

  allOrderStatus: OrderStatus[];
  public orders: Order[];
  bsModalRef: BsModalRef;
  pagination: Pagination;
  orderParams: any = {};
  
  constructor(private adminService: AdminService, private modalService: BsModalService,
  private toastrService: ToastrService) { }

  ngOnInit() {
    this.getOrders();
  }

  //on change pagining
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getOrders();
  }

  
  getOrders() {
    let pageNumber = null;
    let itemsPerPage = null;
    if (this.pagination) {
      pageNumber = this.pagination.currentPage;
      itemsPerPage = this.pagination.itemsPerPage;
    }

    this.adminService.getOrders(pageNumber, itemsPerPage).subscribe((response: any) => {
      if (response.result.returnStatus) {
        this.orders = response.result.entity;
        this.pagination = response.pagination
      }
      else {
        console.log(response.result.returnMessage);
        this.toastrService.error(response.result.returnMessage);
      }
    }, error => {
      console.log(error);
      this.toastrService.error(error.message);
    });
  }

  OpenOrderModal(order: Order) {

    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        order,
        allorderStatus:this.allOrderStatus
      }
    };

    this.bsModalRef = this.modalService.show(OrderDeliveryModalsComponent, config);
    this.bsModalRef.content.updateSelectedOrder.subscribe(values => {
      if (values) {
        this.adminService.updateOrder(order.id, values).subscribe(response => {
          if (response.status == 200)
            this.toastrService.success("order status updated");
          else
            this.toastrService.error("something went wrong");
        })
      }
    })
    //this.bsModalRef = this.modalService.show(CartModalsComponent);
  }
}
