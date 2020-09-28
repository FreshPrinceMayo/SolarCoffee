import axios from 'axios';
import { IInvoice } from "@/types/Invoice";


export class InvoiceService {

public async createInvoice(invoice : IInvoice) : Promise<boolean>
{
    let now = new Date();
    invoice.createdDate = now;
    invoice.updatedDate = now;
    let result : any  = await axios.post("https://localhost:5001/api/invoice/",invoice)
    return result.data;

}


}