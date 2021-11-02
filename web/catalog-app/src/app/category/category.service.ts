import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from './category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  url = environment.url;

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(`${this.url}/Category`);
  }

  add(category: Category) {
    return this.httpClient.post(`${this.url}/Category`, category);
  }

  update(category: Category) {
    return this.httpClient.put(`${this.url}/Category`, category);
  }

  delete(id: string) {
    return this.httpClient.delete(`${this.url}/Category/${id}`);
  }
}
