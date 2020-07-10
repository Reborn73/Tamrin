using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tamrin.Api.Models;
using Tamrin.Data.Contracts;
using Tamrin.Entities.Course;
using Tamrin.WebFramework.Api;

namespace Tamrin.Api.Controllers.V1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class CategoryController : CrudController<CrudCategoryRequestDto, CrudCategoryResponseDto, Category>
    {
        public CategoryController(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
