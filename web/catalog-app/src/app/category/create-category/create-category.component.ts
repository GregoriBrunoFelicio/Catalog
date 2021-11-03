import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { MessageService } from 'src/app/shared/message.service';
import { SharedService } from 'src/app/shared/shared.service';
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
  isSaving = false;

  constructor(
    public modal: NgbActiveModal,
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private sharedService: SharedService
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
    this.isSaving = true;
    this.categoryService.add(category).subscribe(
      (result: any) => {
        this.messageService.showSuccessMessage(result.message);
        this.getCategories();
        this.form.reset();
        this.sharedService.sendMessage();
        this.isSaving = false;
      },
      (errorResponse) => {
        this.isSaving = false;
        this.messageService.showMessageInfo(errorResponse.error);
      }
    );
  }

  update(category: Category) {
    this.isSaving = true;
    this.categoryService.update(category).subscribe(
      (result: any) => {
        this.messageService.showSuccessMessage(result.message);
        this.getCategories();
        this.sharedService.sendMessage();
        this.modal.close();
        this.isSaving = false;
      },
      (errorResponse) => {
        this.messageService.showMessageInfo(errorResponse.error);
        this.isSaving = false;
      }
    );
  }

  delete(id: string) {
    this.isSaving = true;
    this.categoryService.delete(id).subscribe(
      (result: any) => {
        this.getCategories();
        this.messageService.showSuccessMessage(result.message);
        this.sharedService.sendMessage();
        this.isSaving = false;
      },
      (errorResponse) => {
        this.isSaving = false;
        this.messageService.showMessageInfo(errorResponse.error);
      }
    );
  }

  edit(category: Category) {
    this.form.patchValue(category);
  }

  disableSaveButton() {
    return this.form.invalid || this.form.pristine || this.isSaving;
  }
}
