import { Discount } from "./discount";
import { GameConsole } from "./gameConsole";
import { Genres } from "./genres";

export class Product {
    id?: string;
    name?: string;
    status?: string;
    price?: string;
    stock?: string;
    description?: string;
    createdDate?: string;
    modifiedDate?: string;
    console?: GameConsole;
    discount?: Discount;
    genres?: Array<Genres>;
}