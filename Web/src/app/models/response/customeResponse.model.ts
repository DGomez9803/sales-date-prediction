import { BaseResponse } from '../common/baseResponse.model';
import { Pagination } from '../common/pagination.model';
import { Customer } from '../customer.model';
export interface CustomeResponse  extends BaseResponse<Customer> {
  data: Customer;
  pagination: Pagination;
}
