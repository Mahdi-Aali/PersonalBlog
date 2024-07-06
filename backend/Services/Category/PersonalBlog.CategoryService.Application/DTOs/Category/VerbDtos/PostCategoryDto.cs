using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.CategoryService.Application.DTOs.Category.VerbDtos;

public record PostCategoryDto(
    [Required(ErrorMessage = "Category title can't null or empty")]
    [MaxLength(50, ErrorMessage = "Category name can't be more than 50 characters.")]
    [MinLength(2, ErrorMessage = "Category name can't be less than 2 characters.")]
    string Title,

    [Required(ErrorMessage = "Category description can't null or empty")]
    [MaxLength(50, ErrorMessage = "Category description can't be more than 350 characters.")]
    [MinLength(2, ErrorMessage = "Category description can't be less than 3 characters.")]
    string Description);