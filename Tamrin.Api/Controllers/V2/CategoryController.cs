using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Tamrin.Api.Models;
using Tamrin.Data.Contracts;
using Tamrin.Entities.Course;

namespace Tamrin.Api.Controllers.V2
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2")]
    public class CategoryController : V1.CategoryController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoryController(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<IActionResult> Create(CrudCategoryRequestDto dto, CancellationToken cancellationToken)
        {
            return base.Create(dto, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<IActionResult> Update(long id, CrudCategoryRequestDto dto, CancellationToken cancellationToken)
        {
            return base.Update(id, dto, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }
    }
}
