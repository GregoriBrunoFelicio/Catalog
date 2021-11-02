import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { Product } from 'src/app/product/product';
import { SharedService } from 'src/app/shared/shared.service';
import { ProductDetailsComponent } from '../product-details/product-details.component';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  @Input() products$: Observable<Product[]>;
  constructor(
    private modalService: NgbModal,
    private productService: ProductService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.getProducts();
    this.updateProductListListener();
  }

  getProducts() {
    this.products$ = this.productService.getAll();
  }

  updateProductListListener() {
    this.sharedService.getMessage().subscribe((x) => {
      this.getProducts();
    });
  }

  openProductDetailsModal(product: Product) {
    const modalRef = this.modalService.open(ProductDetailsComponent, {
      centered: true,
    });
    modalRef.componentInstance.product = product;
  }
}
