import { BaseResponse } from '../common/baseResponse.model';
import { Pagination } from '../common/pagination.model';
import { Order } from '../order.model';
export interface OrderResponse  extends BaseResponse<Order> {
  data: Order;
  pagination: Pagination;
}
