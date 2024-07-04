using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.CategoryService.Domain.SeedWorker;

public interface IRepository
{
    public IUnitOfWork UnitOfWork { get; }
}
