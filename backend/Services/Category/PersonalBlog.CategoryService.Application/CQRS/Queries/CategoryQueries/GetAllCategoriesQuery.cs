using MediatR;
using Microsoft.AspNetCore.DataProtection;
using PersonalBlog.CategoryService.Application.DTOs.Category;
using PersonalBlog.CategoryService.Application.DTOs.Category.VerbDtos;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Domain.SeedWorker;

namespace PersonalBlog.CategoryService.Application.CQRS.Queries.CategoryQueries;

public class GetAllCategoriesQuery : IRequest<AggregatePagedResult<IEnumerable<CategoryDto>>>
{
    public GetCategoriesDto GetCategoriesDto { get; private set; }
    public IDataProtector DataProtector { get; private set; }

    public GetAllCategoriesQuery(GetCategoriesDto? getCategoriesDto, IDataProtector dataProtector)
    {
        if (getCategoriesDto == null)
        {
            GetCategoriesDto = new GetCategoriesDto(0, 25);
        }
        else
        {
            GetCategoriesDto = getCategoriesDto;
        }


        DataProtector = dataProtector;
    }
}


public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, AggregatePagedResult<IEnumerable<CategoryDto>>>
{
    private readonly ICategoryRepository _repository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _repository = categoryRepository;
    }

    public async Task<AggregatePagedResult<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        
        var result = await _repository.CategoriesAsync(category => category.CategoryVisibilityStatusId == CategoryVisibilityStatus.Enable.Id, 
            AggregatePagedResultSettings.Factory.Create(request.GetCategoriesDto.itemPerPage, request.GetCategoriesDto.pageId)
            , cancellationToken);

        //TODO: use adapter to fix this
        return new(result.PageId, result.ItemPerPage, result.TotalItems, result.Result.Select(x => new CategoryDto(request.DataProtector.Protect(x.Id.ToString()), x.Title)));
    }
}