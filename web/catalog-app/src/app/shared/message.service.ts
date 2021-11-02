import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  constructor(private toastrtService: ToastrService) {}

  showSuccessMessage(message: string) {
    this.toastrtService.success(message, '', {
      positionClass: 'toast-top-right',
      timeOut: 2000,
    });
  }

  showMessageInfo(mensage: string) {
    this.toastrtService.info(mensage, '', {
      positionClass: 'toast-top-right',
      timeOut: 2000,
    });
  }

  showErrorMessage(mensage: string) {
    this.toastrtService.error(mensage, '', {
      positionClass: 'toast-top-right',
      timeOut: 2000,
    });
  }
}
