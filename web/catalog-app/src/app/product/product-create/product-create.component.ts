import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { Category } from 'src/app/category/category';
import { CategoryService } from 'src/app/category/category.service';
import { MessageService } from 'src/app/shared/message.service';
import { SharedService } from 'src/app/shared/shared.service';
import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css'],
})
export class ProductCreateComponent implements OnInit {
  form: FormGroup;
  categories$: Observable<Category[]>;

  constructor(
    public modal: NgbActiveModal,
    private productService: ProductService,
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private sharedService: SharedService
  ) {}

  ngOnInit() {
    this.createForm();
    this.getCategories();
  }

  createForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.maxLength(30)]],
      price: ['', [Validators.required]],
      categoryId: ['', Validators.required],
      category: [null],
    });
  }

  getCategories() {
    this.categories$ = this.categoryService.getAll();
  }

  save() {
    if (this.form.invalid || this.form.pristine) return;
    const product = this.form.value as Product;

    if (!product.id) {
      this.add(product);
    } else {
      this.update(product);
    }
  }

  add(product: Product) {
    this.productService.add(product).subscribe(
      (result: any) => {
        this.messageService.showSuccessMessage(result.message);
        this.form.reset();
        this.sharedService.sendMessage();
      },
      (errorResponse) => {
        this.messageService.showMessageInfo(errorResponse.error);
      }
    );
  }

  update(product: Product) {
    this.productService.update(product).subscribe(
      (result: any) => {
        this.messageService.showSuccessMessage(result.message);
        this.sharedService.sendMessage();
        this.modal.close();
      },
      (errorResponse) => {
        this.messageService.showMessageInfo(errorResponse.error);
      }
    );
  }

  edit(product: Product) {
    setTimeout(() => {
      this.form.patchValue(product);
    }, 1);
  }
}
