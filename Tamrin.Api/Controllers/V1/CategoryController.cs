using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Tamrin.Api.Models;
using Tamrin.Data.Contracts;
using Tamrin.Entities.Course;
using Tamrin.WebFramework.Api;

namespace Tamrin.Api.Controllers.V1
{
    [ApiVersion("1")]
    public class CategoryController : CrudController<CrudCategoryRequestDto, CrudCategoryResponseDto, Category>
    {

        public CategoryController(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }


        public override Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }


        public override Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }


        public override Task<IActionResult> Create(CrudCategoryRequestDto dto, CancellationToken cancellationToken)
        {
            return base.Create(dto, cancellationToken);
        }


        public override Task<IActionResult> Update(long id, CrudCategoryRequestDto dto, CancellationToken cancellationToken)
        {
            return base.Update(id, dto, cancellationToken);
        }


        public override Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }
    }
}
