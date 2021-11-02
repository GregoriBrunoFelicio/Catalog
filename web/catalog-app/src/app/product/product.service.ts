import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from './product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  url = environment.url;

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(`${this.url}/Product`);
  }

  add(product: Product) {
    return this.httpClient.post(`${this.url}/Product`, product);
  }

  update(product: Product) {
    return this.httpClient.put(`${this.url}/Product`, product);
  }

  delete(id: string) {
    return this.httpClient.delete(`${this.url}/Category/${id}`);
  }
}
