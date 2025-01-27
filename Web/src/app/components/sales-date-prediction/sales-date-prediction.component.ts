import { Component, OnInit } from '@angular/core';
import { Pagination } from '../../models/common/pagination.model';
import { Customer } from '../../models/customer.model';
import { ServicesCustomer } from '../../services/servicesCustomer';
import { OrdersComponent } from "../orders/orders.component";
import { NewOrderComponent } from "../new-order/new-order.component";
import { D3Component } from "../d3/d3.component";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sales-date-prediction',
  imports: [OrdersComponent, NewOrderComponent, D3Component, FormsModule],
  templateUrl: './sales-date-prediction.component.html',
  styleUrl: './sales-date-prediction.component.css'
})
export class SalesDatePredictionComponent implements OnInit {
  public filteredCustomers: Customer[] = [];
  public searchQuery = '';
  public selectedCustomerId = '';
  public textPageCurrent: string = "";
  public pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 10,
    totalPages: 1
  };
  public pageOptions: number[] = [];;
  public isVisibleOrders: boolean = false;
  public isVisibleNewOrder: boolean = false;
  public isVisibleD3: boolean = false;
  private customers: Customer[] = [];

  constructor(private servicesCustomer: ServicesCustomer) {}

  public ngOnInit(): void {
    this.loadCustomers();
  }

  public filterCustomers(): void {
    this.loadCustomers();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalPages: 1
    };


    this.updatePagination();
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

  public viewOrders(customerId: number): void {
    this.selectedCustomerId = customerId.toString();
    this.isVisibleOrders = true;
  }

  public viewD3(): void {
    this.isVisibleD3 = true;
  }

  public createOrder(customerId: number): void {
    this.selectedCustomerId = customerId.toString();
    this.isVisibleNewOrder = true;
  }

  public closeModalOrders() {
    this.isVisibleOrders = false;
  }

  public closeModalD3() {
    this.isVisibleD3 = false;
  }


  public closeModalNewOrder() {
    this.isVisibleNewOrder = false;
  }

  private loadCustomers(): void {
    this.servicesCustomer.getCustomers(this.searchQuery).subscribe((data) => {
      this.customers = data.data;
      this.filteredCustomers = data.data;
      this.updatePagination();
    });
  }

  private updatePagination(): void {
    this.pagination.totalPages = Math.ceil(this.filteredCustomers.length / this.pagination.itemsPerPage);
    this.pageOptions = Array.from({ length: this.pagination.totalPages }, (_, index) => index + 1);
    this.changePage();
  }

  private changePage(): void {
    const start = (this.pagination.currentPage - 1) * this.pagination.itemsPerPage;
    const end = start + this.pagination.itemsPerPage;
    this.textPageCurrent =  ` ${start + 1}-${end} of ${this.customers.length}`;
    this.filteredCustomers = this.customers.slice(start, end);
  }
}
