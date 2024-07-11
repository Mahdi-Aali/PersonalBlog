import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  categoryId: string = "";
  categoryName: string = "";
  constructor(route: ActivatedRoute) {
    this.categoryId = route.snapshot.paramMap.get("id")!;
    this.categoryName = route.snapshot.paramMap.get("title")!;
  }
}
