using MediatR;
using PersonalBlog.CategoryService.Application.DTOs.Category;
using PersonalBlog.CategoryService.Application.DTOs.Category.VerbDtos;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

namespace PersonalBlog.CategoryService.Application.CQRS.Commands.CategoryCommands;

public class AddNewCategoryCommand : IRequest<CategoryInsertedDto>
{
    public PostCategoryDto Dto { get; private set; }

    public AddNewCategoryCommand(PostCategoryDto dto) => Dto = dto;
}



public class AddNewCategoryCommandHandler : IRequestHandler<AddNewCategoryCommand, CategoryInsertedDto>
{
    private readonly ICategoryRepository _repository;

    public AddNewCategoryCommandHandler(ICategoryRepository repository) => _repository = repository;


    public async Task<CategoryInsertedDto> Handle(AddNewCategoryCommand request, CancellationToken cancellationToken)
    {
        Category result = await _repository.AddAsync(
            Category.Factory.Create(request.Dto.Title, request.Dto.Description), cancellationToken);

        await _repository.UnitOfWork.SaveEntitiesAsync(null, cancellationToken);

        return new(result.Id, result.Title, result.Description);
    }
}