import { OrderStatus } from "./order-status";
import { User } from "./user";

export class Order {
  id: number;
  status: OrderStatus;
  user: User;
  totalprice: number;
  shipmentaddress: string;
}
