import { Injectable } from "@angular/core";
import { ApiResponse } from '../CommonDtos/ApiResponse';
import { ServiceBase } from "../ServiceBase";
import { GetCategoriesDto } from './Dtos/GetCategoriesDto';
import { Observable } from "rxjs";

@Injectable({providedIn: "root"})
export class CategoryService extends ServiceBase
{

  public GetAllCategories() : Observable<ApiResponse<GetCategoriesDto>>
  {
    return this._httpClient!.get<ApiResponse<GetCategoriesDto>>("https://localhost:7183/api/categories");
  }
}
