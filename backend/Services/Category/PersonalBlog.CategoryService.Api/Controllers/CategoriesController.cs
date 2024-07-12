using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PersonalBlog.CategoryService.Api.Helpers;
using PersonalBlog.CategoryService.Application.CQRS.Commands.CategoryCommands;
using PersonalBlog.CategoryService.Application.CQRS.Queries.CategoryQueries;
using PersonalBlog.CategoryService.Application.DTOs.Category;
using PersonalBlog.CategoryService.Application.DTOs.Category.VerbDtos;
using PersonalBlog.CategoryService.Domain.SeedWorker;

namespace PersonalBlog.CategoryService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ApiControllerBase
{
    private readonly ISender _sender;
    private readonly IDataProtector _dataProtector;
    private readonly Logging.ILogger _logger;

    public CategoriesController(IDistributedCache cache, ISender sender, IDataProtectionProvider dataProtectionProvider, Logging.ILogger logger) : base(cache)
    {
        _sender = sender;
        _dataProtector = dataProtectionProvider.CreateProtector(nameof(CategoriesController));
        _logger = logger;
    }


    [HttpGet]
    [Consumes(typeof(GetCategoriesDto), "application/json")]
    [ProducesResponseType(typeof(ApiResponse<AggregatePagedResult<IEnumerable<CategoryDto>>>), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<AggregatePagedResult<IEnumerable<CategoryDto>>>), StatusCodes.Status404NotFound, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<AggregatePagedResult<IEnumerable<CategoryDto>>>), StatusCodes.Status400BadRequest, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<AggregatePagedResult<IEnumerable<CategoryDto>>>), StatusCodes.Status403Forbidden, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<AggregatePagedResult<IEnumerable<CategoryDto>>>), StatusCodes.Status500InternalServerError, "application/json")]
    [Produces(typeof(ApiResponse<AggregatePagedResult<IEnumerable<CategoryDto>>>))]
    public async Task<IActionResult> GetAsync([FromQuery] GetCategoriesDto dto)
    {
        ApiResponseBuilder<AggregatePagedResult<IEnumerable<CategoryDto>>> responseBuilder = new();
        if (dto != null && ModelState.IsValid)
        {
            return Ok(responseBuilder
                .SetHeaders(new(StatusCodes.Status200OK, ["OK"]))
                .SetPayload(new(await _sender.Send(new GetAllCategoriesQuery(dto, _dataProtector))))
                .Build());
        }
        else
        {
            return BadRequest(

                responseBuilder
                .SetHeaders(new(StatusCodes.Status400BadRequest, ModelState.SelectMany(s => s.Value?.Errors.Select(error => error.ErrorMessage)!).ToArray()))
                .SetPayload(new ApiResponsePayload<AggregatePagedResult<IEnumerable<CategoryDto>>>(null!)
                ).Build()

                );
        }
    }



    [HttpPost]
    [Consumes(typeof(PostCategoryDto), "application/json")]
    [Produces(typeof(ApiResponse<CategoryInsertedDto>))]
    [ProducesResponseType(typeof(ApiResponse<CategoryInsertedDto>), StatusCodes.Status201Created, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<CategoryInsertedDto>), StatusCodes.Status400BadRequest, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<CategoryInsertedDto>), StatusCodes.Status422UnprocessableEntity, "application/json")]
    [ProducesResponseType(typeof(ApiResponse<CategoryInsertedDto>), StatusCodes.Status403Forbidden, "application/json")]
    public async Task<IActionResult> PostAsync([FromBody] PostCategoryDto dto)
    {
        ApiResponseBuilder<CategoryInsertedDto> responseBuilder = new();

        if (ModelState.IsValid)
        {
            CategoryInsertedDto categoryInsertedDto = await _sender.Send(new AddNewCategoryCommand(dto));
            if (categoryInsertedDto != null)
            {
                return Created(string.Empty,
                responseBuilder
                .SetHeaders(new(StatusCodes.Status201Created, ["Created"]))
                .SetPayload(new(categoryInsertedDto))
                .Build()
                );
            }
            return UnprocessableEntity(
                responseBuilder
                .SetHeaders(new(StatusCodes.Status422UnprocessableEntity, ["It looks like something went wrong. please try again later."]))
                .SetPayload(new(null!))
                .Build()
                );
        }
        return BadRequest(
            responseBuilder
            .SetHeaders(new(StatusCodes.Status400BadRequest, ModelState.SelectMany(s => s.Value?.Errors.Select(error => error.ErrorMessage)!).ToArray()))
            .SetPayload(new(null!))
            .Build()
            );
    }

}