import { Component, OnInit } from "@angular/core";
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";

import { Product } from "../Shared/Entities/product";
import { ControlPanelService } from "../Shared/Services/controlPanel.service";

@Component({
    templateUrl: "products.component.html"
})

export class ProductsComponent implements OnInit{
    addProductForm!: FormGroup;
    imageUrl!: string;
    loading = false;
    submitted = false;
    products?: Array<Product>;
    openAccordion: Array<boolean> = [];
    isEditable: Array<boolean> = [];

    constructor(
        private controlPanelService: ControlPanelService,
        private formBuilder : FormBuilder,
    ) { 
        this.getTable();
    }
    
    get values() {
        return this.addProductForm.controls;
    }

    get genresArray(): FormArray {
        return this.addProductForm.get('genresArray') as FormArray;
    }
    
    get imageControls() {
      return this.addProductForm.get('images') as FormArray;
    }
    
    ngOnInit(){
      this.addProductForm = this.formBuilder.group({
        name: [null, Validators.required],
        price: [null, Validators.required],
        stock: [null, Validators.required],
        console: [null, Validators.required],
        description: [null, Validators.required],
        genresArray: this.formBuilder.array(['']),
        images: this.formBuilder.array([]),
      });
      this.addImage();
    }

    
    addGenre() {
      this.genresArray.push(this.formBuilder.control(''));
    }

    removeGenre(i: number) {
      this.genresArray.removeAt(i);
    }

    addImage() {
      const imageGroup = this.formBuilder.group({
        imageFile: [''],
        imageUrl: [''],
      });
      this.imageControls.push(imageGroup);
    }
    
    onFileSelected(event: any, index: number) {
      const file = event.target.files[0];
      if (file) {
        this.imageControls.at(index).get('imageFile')!.setValue(file);
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.imageControls.at(index).get('imageUrl')!.setValue(e.target.result);
        };
        reader.readAsDataURL(file);
      }
    }

    getImageUrl(imageControl: AbstractControl) {
      const imageUrl = imageControl.get('imageUrl')!.value;
      return imageUrl ? imageUrl : null;
    }
    
    removeImage(i: number){
      this.imageControls.removeAt(i);
    }
    
    getTable() {
        this.controlPanelService.getAllProducts()
            .subscribe({
                next: (data : Product[]) => {
                    this.products = data;
                    console.log(data);
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

    addNewProduct() {
        const newProduct = new FormData();

        newProduct.append("name", this.addProductForm.get("name")?.value); 
        console.log(this.addProductForm.value); 
    }
 }

