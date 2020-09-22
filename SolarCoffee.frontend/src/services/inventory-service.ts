import axios from 'axios';
import {IProductInventory} from "@/types/Product";
import { IShipment } from "@/types/Shipment";

export class InventoryService {
    // const API_URL = "https://localhost:5000/api";

    public async getInventory() : Promise<IProductInventory[]> {
        // console.log(API_URL);
        let result = await axios.get(`https://localhost:5001/api/inventory/`);

        console.log(result);

        return result.data;
    }

    public async updateInventoryQuantity(shipment : IShipment)
    {
        let result = await axios.patch(`https://localhost:5001/api/inventory/`,shipment);
        console.log(result);
        return result.data;
    }
}