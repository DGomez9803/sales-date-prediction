import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ServicesCustomer } from '../../services/servicesCustomer';
import { Pagination } from '../../models/common/pagination.model';
import { Order } from '../../models/order.model';

@Component({
  selector: 'app-orders',
  imports: [],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent implements OnInit {
  public filteredOrders: Order[] = [];
  public textPageCurrent: string = "";
  public pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 9,
    totalPages: 1
  };
  public pageOptions: number[] = [];;
  get customerId(): string {
    return this._customerId;
  }

  @Input()
  set customerId(value: string) {
    this._customerId = value;
    this.loadOrders();
  }
  private _customerId: string = '0';
  private orders: Order[] = [];
  @Output() closeModal = new EventEmitter<any>();

  constructor(private servicesCustomer: ServicesCustomer) {}

  public ngOnInit(): void {
    this.loadOrders();
  }

  private loadOrders(): void {
    this.servicesCustomer.getOrderByCustomers(this.customerId).subscribe((data) => {
      this.orders = data.data;
      this.filteredOrders = data.data;
      this.updatePagination();
    });
  }

  public changePageSelect(event: any) {
    this.pagination.currentPage = +event.target.value;
    this.changePage();
  }

  public nextPage(): void {
    if (this.pagination.currentPage < this.pagination.totalPages) {
      this.pagination.currentPage++;
      this.changePage();
    }
  }

  public previousPage(): void {
    if (this.pagination.currentPage > 1) {
      this.pagination.currentPage--;
      this.changePage();
    }
  }

  public close(): void {
    this.closeModal.emit();
  }

  private updatePagination(): void {
    this.pagination.totalPages = Math.ceil(this.filteredOrders.length / this.pagination.itemsPerPage);
    this.pageOptions = Array.from({ length: this.pagination.totalPages }, (_, index) => index + 1);
    this.changePage();
  }

  private changePage(): void {
    const start = (this.pagination.currentPage - 1) * this.pagination.itemsPerPage;
    const end = start + this.pagination.itemsPerPage;
    this.textPageCurrent =  ` ${start + 1}-${end} of ${this.orders.length}`;
    this.filteredOrders = this.orders.slice(start, end);
  }
}
