export interface IProduct {
    id: number;
    createdDate:Date;
    updatedDate:Date;
    name: string;
    description: string;
    price: number;
    isTaxable: boolean;
    isArchived;
}


export interface IProductInventory
{
    id:number;
    product:IProduct;
    quantityOnHand: number;
    idealQuantity: number;
}