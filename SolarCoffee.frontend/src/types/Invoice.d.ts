  
import { IProduct } from "@/types/Product";

export interface IInvoice {
  customerId: number;
  lineItems: ILineItem[];
  createdDate: Date;
  updatedDate: Date;
}

export interface ILineItem {
  product?: IProduct;
  quantity: number;
}