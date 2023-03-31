import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environment/environment";
import { Product } from "../Entities/product";
import { User } from "../Entities/user";

@Injectable({ providedIn: 'root' })

export class ControlPanelService {
    constructor(
        private http : HttpClient
    ) { }

    getAllUsers() {
        return this.http.get<Array<User>>(`${environment.apiUrl}/auth/getall`);
    }

    deleteUser(id: string) {
        return this.http.delete<User>(`${environment.apiUrl}/auth/deletebyID/${id}`);
    }

    updateUser(id: string, user: User) {
        return this.http.put<User>(`${environment.apiUrl}/auth/update/${id}`, user);
    }

    getAllProducts() {
        return this.http.get<Array<Product>>(`${environment.apiUrl}/product/getall`);
    }

    deleteProduct(id: string){
        return this.http.delete<Product>(`${environment.apiUrl}/product/deletebyID/${id}`);
    }
 }