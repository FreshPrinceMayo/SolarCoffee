import axios from 'axios';
import { ICustomer } from "@/types/Customer";

export class CustomerService {

    public async getCustomers() : Promise<ICustomer[]> {
        let result = await axios.get(`https://localhost:5001/api/customer/`);
        console.log(result);
        return result.data;
    }

}