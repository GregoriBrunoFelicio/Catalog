import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Product } from 'src/app/models/product';
import { ProductDetailsComponent } from '../product-details/product-details.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  constructor(private modalService: NgbModal) {
    this.products = [
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
      { name: 'Pão de Queijo', price: 5 } as Product,
    ];
  }

  ngOnInit(): void {}

  openProductDetailsModal() {
    this.modalService.open(ProductDetailsComponent, {
      centered: true,
    });
  }
}
