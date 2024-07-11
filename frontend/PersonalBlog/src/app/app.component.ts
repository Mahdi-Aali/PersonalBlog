import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CategoryService } from '../services/Categories/CategoryService';
import { ApiResponse } from '../services/CommonDtos/ApiResponse';
import { Observable } from 'rxjs';
import { GetCategoriesDto } from '../services/Categories/Dtos/GetCategoriesDto';
import { Category } from '../services/Categories/Dtos/Category';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'PersonalBlog';
  categories: Category[] | undefined;

  constructor(public categoryService: CategoryService) {
    this.GetCategories().subscribe(async result =>
      {
        this.categories = await result.payload.body.result;
      }
    );
  }

  GetCategories(): Observable<ApiResponse<GetCategoriesDto>>
  {
    return this.categoryService.GetAllCategories();
  }

}
