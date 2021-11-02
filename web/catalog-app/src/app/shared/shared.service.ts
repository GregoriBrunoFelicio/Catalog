import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  updateCategories = new Subject<boolean>();

  sendMessage() {
    this.updateCategories.next(true);
  }

  getMessage() {
    return this.updateCategories.asObservable();
  }
}
