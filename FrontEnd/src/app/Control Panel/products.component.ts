import { Component } from "@angular/core";
import { FormBuilder } from "@angular/forms";

import { Product } from "../Shared/Entities/product";
import { ControlPanelService } from "../Shared/Services/controlPanel.service";

@Component({
    templateUrl: "products.component.html"
})

export class ProductsComponent {
    products?: Array<Product>;
    openAccordion: Array<boolean> = [];
    isEditable: Array<boolean> = [];

    constructor(
        private controlPanelService: ControlPanelService,
        private formBuilder : FormBuilder,
    ) { 
        this.getTable();
    }

    getTable() {
        this.controlPanelService.getAllProducts()
            .subscribe({
                next: (data : Product[]) => {
                    this.products = data;
                }
            });
    }

    delete(id: string) {
        this.controlPanelService.deleteProduct(id)
            .subscribe({
                next: () => {
                    this.getTable();
                }
            });
    }
 }