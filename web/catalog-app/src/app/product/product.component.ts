import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { Category } from '../category/category';
import { CategoryService } from '../category/category.service';
import { CreateCategoryComponent } from '../category/create-category/create-category.component';
import { Product } from './product';
import { ProductCreateComponent } from './product-create/product-create.component';
import { ProductService } from './product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
  categories$: Observable<Category[]>;
  products$: Observable<Product[]>;

  constructor(
    private modalService: NgbModal,
    private categoryService: CategoryService,
    private productService: ProductService
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

  getByCategory(categoryId: string) {
    if (categoryId) {
      this.products$ = this.productService.getByCategory(categoryId);
    } else {
      this.products$ = this.productService.getAll();
    }
  }
}
