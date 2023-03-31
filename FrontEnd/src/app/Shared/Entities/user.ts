import { Role } from "./role";

export class User {
    id?: string;
    username?: string;
    password?: string;
    firstName?: string;
    lastName?: string;
    email?: string;
    socialSecurity?: number;
    phoneNumber?: number;
    address?: string;
    city?: string;
    country?: string;
    postalCode?: string;
    token?: string;
    role?: Role;
    createdDate?: string;
    modifiedDate?: string;
}
