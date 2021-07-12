import { Cart } from "./cart";
import { Product } from "./product";

export class CartItem {
  id: number;
  product: Product;
  cart: Cart;
  qunatity: number;
  name: string;
  price: number;
}
