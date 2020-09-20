import axios from 'axios';
import {IProductInventory} from "@/types/Product";

export class InventoryService {
    // const API_URL = "https://localhost:5000/api";

    public async getInventory() : Promise<IProductInventory[]> {
        // console.log(API_URL);
        let result = await axios.get(`https://localhost:5001/api/inventory/`);

        console.log(result);

        return result.data;
    }
}