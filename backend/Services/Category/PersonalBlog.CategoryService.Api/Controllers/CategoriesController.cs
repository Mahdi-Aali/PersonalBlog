using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PersonalBlog.BuildingBlocks.Extenssions.Tasks;

namespace PersonalBlog.CategoryService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ApiControllerBase
{
    public CategoriesController(IDistributedCache cache) : base(cache)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return await Task.Run(() =>
        {
            return Ok(new string[] { "Leet coding", "DDD", "C#", "ASP.NET (core)", "Angular" });
        })
            .WithTimeout(TimeSpan.FromSeconds(1));
    }
}