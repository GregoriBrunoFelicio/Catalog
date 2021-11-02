import { Category } from '../category/category';

export interface Product {
  id: string;
  name: string;
  price: number;
  category: Category;
}
