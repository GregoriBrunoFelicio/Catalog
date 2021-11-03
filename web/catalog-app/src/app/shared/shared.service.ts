import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  updateState = new Subject<boolean>();

  sendMessage() {
    this.updateState.next(true);
  }

  getMessage() {
    return this.updateState.asObservable();
  }
}
