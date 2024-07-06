using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.CategoryService.Application.DTOs.Category.VerbDtos;

public record GetCategoriesDto([Required(ErrorMessage = "Page id can't be null")] int pageId = 0, [Required(ErrorMessage = "please specify item per page.")] int itemPerPage = 25);