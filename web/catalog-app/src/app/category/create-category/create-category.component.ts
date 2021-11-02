import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
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
    private formBuilder: FormBuilder
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
      name: ['', [Validators.required, Validators.maxLength(30)]],
    });
  }

  save() {
    if (this.form.invalid) return;

    const category = this.form.value;

    this.categoryService.add(category).subscribe(
      (result: any) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
