using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PersonalBlog.BuildingBlocks.Extenssions.Tasks;
using System.Text.Json;

namespace PersonalBlog.CategoryService.Api.Controllers;

public class ApiControllerBase : ControllerBase
{
    private IDistributedCache _cache;

    public ApiControllerBase(IDistributedCache cache) => _cache = cache;


    public virtual async Task SetToCacheAsync(string key, object value)
    {
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value)).WithTimeout(TimeSpan.FromSeconds(2));
    }

    public virtual async Task<TResult> GetFromCacheAsync<TResult>(string key)
    {
        return JsonSerializer.Deserialize<TResult>(await _cache.GetStringAsync(key).WithTimeout(TimeSpan.FromSeconds(2)) ?? "") ?? default(TResult)!;
    }
}