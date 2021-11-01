import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  constructor() {
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
}
