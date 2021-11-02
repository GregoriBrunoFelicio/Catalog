import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { MessageService } from 'src/app/shared/message.service';
import { Category } from '../category';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css'],
})
export class CreateCategoryComponent implements OnInit {
  categories$: Observable<Category[]>;
  form: FormGroup;

  constructor(
    public modal: NgbActiveModal,
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.createForm();
    this.getCategories();
  }

  getCategories() {
    this.categories$ = this.categories$ = this.categoryService.getAll();
  }

  createForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.maxLength(30)]],
    });
  }

  save() {
    if (this.form.invalid || this.form.pristine) return;
    const category = this.form.value;
    if (!category.id) {
      this.add(category);
    } else {
      this.update(category);
    }
  }

  add(category: Category) {
    this.categoryService.add(category).subscribe(
      (result: any) => {
        this.messageService.showSuccessMessage('Criou');
        this.getCategories();
        this.form.reset();
        console.log('chamou');
      },
      (error) => {
        this.messageService.showErrorMessage('Error');
        console.log('ERRRO');
      }
    );
  }

  update(category: Category) {
    this.categoryService.update(category).subscribe(
      (result: any) => {
        this.getCategories();
        this.form.reset();
      },
      (error) => {
        console.log('ERRRO');
      }
    );
  }

  delete(id: string) {
    this.categoryService.delete(id).subscribe(
      () => {
        this.getCategories();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  edit(category: Category) {
    this.form.patchValue(category);
  }
}
