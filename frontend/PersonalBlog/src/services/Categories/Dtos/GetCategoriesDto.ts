import { Category } from "./Category";

export interface GetCategoriesDto
{
  pageId: number;
  itemPerPage: number;
  totalItems: number;
  result: Category[];
}

