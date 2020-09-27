export interface ICustomer {
  id: number;
  createdDate: Date;
  updatedDate?: Date;
  firstName: string;
  lastName: string;
  primaryAddress: ICustomerAddress;
}

export interface ICustomerAddress {
  id: number;
  createdDate: Date;
  updatedDate?: Date;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
}