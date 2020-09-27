import axios from 'axios';
import { ICustomer } from "@/types/Customer";
import { IServiceResponse } from "@/types/ServiceResponse";

export class CustomerService {

    public async getCustomers() : Promise<ICustomer[]> {
        let result : any = await axios.get(`https://localhost:5001/api/customer/`);
        console.log(result);
        return result.data;
    }

    public async save(newCustomer : ICustomer) : Promise<IServiceResponse<ICustomer>> {
        let result: any = await axios.post(`https://localhost:5001/api/customer/`,newCustomer);
        return result.data;
    }

    public async delete(customerId : number) {
        let result : any = await axios.delete(`https://localhost:5001/api/customer/${customerId}`);
        return result.data;
    }

}