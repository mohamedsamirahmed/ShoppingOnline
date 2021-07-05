import { Category } from "./category";
import { Photo } from "./photo";

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  qunatity: number;
  category: Category;
  products: Photo[];
}

