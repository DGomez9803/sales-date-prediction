import { Routes } from '@angular/router';
import { NewOrderComponent } from './components/new-order/new-order.component';
import { OrdersComponent } from './components/orders/orders.component';
import { SalesDatePredictionComponent } from './components/sales-date-prediction/sales-date-prediction.component';

export const routes:  Routes = [
  { path: '', redirectTo: 'sales-date-prediction', pathMatch: 'full' },
  { path: 'sales-date-prediction', component: SalesDatePredictionComponent },
];
