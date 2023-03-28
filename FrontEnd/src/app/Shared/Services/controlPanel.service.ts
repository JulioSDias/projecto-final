import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environment/environment";
import { User } from "../Entities/user";

@Injectable({ providedIn: 'root' })

export class ControlPanelService {
    constructor(
        private http : HttpClient
    ) { }

    getAllUsers() {
        return this.http.get<Array<User>>(`${environment.apiUrl}/auth/getall`);
    }
 }