import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { Category } from '../category/category';
import { CategoryService } from '../category/category.service';
import { CreateCategoryComponent } from '../category/create-category/create-category.component';
import { ProductCreateComponent } from './product-create/product-create.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
  categories$: Observable<Category[]>;

  constructor(
    private modalService: NgbModal,
    private categoryService: CategoryService
  ) {}

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.categories$ = this.categoryService.getAll();
  }

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
