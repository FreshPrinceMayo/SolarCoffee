import axios from 'axios';
import { ICustomer } from "@/types/Customer";

export class CustomerService {

    public async getCustomers() : Promise<ICustomer[]> {
        let result = await axios.get(`https://localhost:5001/api/customer/`);
        console.log(result);
        return result.data;
    }

    public async save(newCustomer : ICustomer) {
        let result = await axios.post(`https://localhost:5001/api/customer/`,newCustomer);
        return result.data;
    }

    public async delete(customerId : number) {
        let result = await axios.delete(`https://localhost:5001/api/customer/${customerId}`);
        return result.data;
    }

}