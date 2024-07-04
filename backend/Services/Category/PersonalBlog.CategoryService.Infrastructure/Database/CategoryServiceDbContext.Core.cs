using Microsoft.EntityFrameworkCore;

namespace PersonalBlog.CategoryService.Infrastructure.Database;

public sealed partial class CategoryServiceDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
