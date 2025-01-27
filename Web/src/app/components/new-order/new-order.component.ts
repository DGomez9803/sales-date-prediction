import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ServicesResources } from '../../services/servicesResources ';
import { SelectItem } from '../../models/common/selectItem.model';
import { OrderRequest } from '../../models/request/orderRequest.model';
import { ServicesCustomer } from '../../services/servicesCustomer';

@Component({
  selector: 'app-new-order',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './new-order.component.html',
  styleUrl: './new-order.component.css',
})
export class NewOrderComponent implements OnInit {

  public orderForm!: FormGroup;
  public listEmployees: SelectItem[] = [];
  public listShippers: SelectItem[] = [];
  public listProducts: SelectItem[] = [];
  public orderRequest: OrderRequest = {
    empId: 0!,
    shipperId: 0!,
    shipName: undefined!,
    shipAddress: undefined!,
    shipCity: undefined!,
    shipCountry: undefined!,
    orderDate: undefined!,
    requiredDate: undefined!,
    shippedDate: undefined!,
    freight: undefined!,
    productId: 0!,
    unitPrice: undefined!,
    quantity: undefined!,
    discount: undefined!,
    custId: undefined!,
  };

  get customerId(): string {
    return this._customerId;
  }

  @Input()
  set customerId(value: string) {
    this._customerId = value;
    this.orderRequest.custId = parseInt(this.customerId, 10) ;
  }
  private _customerId: string = '0';
  @Output() closeForm = new EventEmitter<any>();

  constructor(
    private servicesResources: ServicesResources,
    private servicesCustomer: ServicesCustomer,
    private fb: FormBuilder) {}

  public ngOnInit(): void {
    this.getEmployees();
    this.getShipperss();
    this.getProducts();
    this.setOrderForm();
    this.orderRequest.custId = parseInt(this.customerId, 10) ;
  }

  public saveForm(): void {
    if (this.orderForm.valid) {
      this.servicesCustomer.createOrder(this.orderRequest).subscribe((data) => {
        this.close();
      });
    } else {
      this.orderForm.markAllAsTouched();
    }
  }

  public close(): void {
    this.closeForm.emit();
  }

  private getEmployees(): void {
    this.servicesResources.getEmployees().subscribe((data) => {
      this.listEmployees = data.data;
    });
  }

  private getShipperss(): void {
    this.servicesResources.getShippers().subscribe((data) => {
      this.listShippers = data.data;
    });
  }

  private getProducts(): void {
    this.servicesResources.getProducts().subscribe((data) => {
      this.listProducts = data.data;
    });
  }

  private setOrderForm(): void {
    this.orderForm = this.fb.group({
      empId: ['', Validators.required],
      shipperId: ['', Validators.required],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      shipCountry: ['', Validators.required],
      orderDate: ['', Validators.required],
      requiredDate: ['', Validators.required],
      shippedDate: ['', Validators.required],
      freight: [Validators.required, Validators.pattern('^[0-9]+$')],
      productId: ['', Validators.required],
      unitPrice: [Validators.required, Validators.pattern('^[0-9]+$')],
      quantity: [Validators.required, Validators.pattern('^[0-9]+$')],
      discount: [Validators.required, Validators.pattern('^[0-9]+$')]
    });
    this.orderForm.reset({}, { emitEvent: false });
  }
}
