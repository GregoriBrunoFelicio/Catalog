import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateCategoryComponent } from '../category/create-category/create-category.component';
import { ProductCreateComponent } from './product-create/product-create.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
  constructor(private modalService: NgbModal) {}

  ngOnInit(): void {}

  openCreateProductModal() {
    this.modalService.open(ProductCreateComponent, {
      centered: true,
    });
  }

  openCreateCategoryModal() {
    this.modalService.open(CreateCategoryComponent, {
      centered: true,
      scrollable: true,
    });
  }
}
