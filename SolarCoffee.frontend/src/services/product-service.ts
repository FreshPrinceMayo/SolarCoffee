import axios from 'axios';
import { IProduct } from "@/types/Product";

export class ProductService {

    public async archive(productId : number) {
        let result = await axios.patch(`https://localhost:5001/api/product/${productId}`);
        return result.data;
    }

    public async save(newProduct : IProduct) {
        let result = await axios.post(`https://localhost:5001/api/product/`, newProduct);
        return result.data;
    }

}

