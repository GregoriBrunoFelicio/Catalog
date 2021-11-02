import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css'],
})
export class CreateCategoryComponent implements OnInit {
  constructor(public modal: NgbActiveModal) {}

  ngOnInit(): void {}

  save() {
    this.modal.close();
  }
}
