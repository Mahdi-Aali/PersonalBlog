import { Routes } from '@angular/router';
import { CategoryComponent } from '../components/category/category.component';

export const routes: Routes = [{
  path: "category/:id/:title",
  component: CategoryComponent
}];
