import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Product } from '../product';
import { ProductCreateComponent } from '../product-create/product-create.component';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit {
  product: Product;

  constructor(public modal: NgbActiveModal, private modalService: NgbModal) {}

  ngOnInit() {}

  openEditProductModal() {
    this.modal.close();
    const modalRef = this.modalService.open(ProductCreateComponent, {
      centered: true,
    });

    modalRef.componentInstance.edit(this.product);
  }
}
