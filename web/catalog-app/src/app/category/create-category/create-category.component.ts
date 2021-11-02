import { Component, OnInit } from '@angular/core';
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

  constructor(
    public modal: NgbActiveModal,
    private categoryService: CategoryService
  ) {}

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.categories$ = this.categories$ = this.categoryService.getAll();
  }

  save() {
    this.modal.close();
  }
}
