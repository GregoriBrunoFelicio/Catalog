import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
    const modalRef = this.modalService.open(ProductCreateComponent, {
      centered: true,
    });
  }
}
