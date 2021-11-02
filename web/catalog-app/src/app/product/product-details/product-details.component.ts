import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'src/app/shared/message.service';
import { Product } from '../product';
import { ProductCreateComponent } from '../product-create/product-create.component';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit {
  product: Product;

  constructor(
    public modal: NgbActiveModal,
    private modalService: NgbModal,
    private productService: ProductService,
    private messageService: MessageService
  ) {}

  ngOnInit() {}

  openEditProductModal() {
    this.modal.close();
    const modalRef = this.modalService.open(ProductCreateComponent, {
      centered: true,
    });

    modalRef.componentInstance.edit(this.product);
  }

  deleteProduct(id: string) {
    this.productService.delete(id).subscribe(
      (result: any) => {
        this.messageService.showSuccessMessage(result.message);
        this.modal.close();
      },
      (errorResponse) => {
        this.messageService.showMessageInfo(errorResponse.error);
      }
    );
  }
}
