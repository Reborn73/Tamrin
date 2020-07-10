using Microsoft.AspNetCore.Mvc;
using Tamrin.WebFramework.Filters;

namespace Tamrin.WebFramework.Api
{
    [ApiController]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        public bool UserIsAuthenticated => HttpContext.User.Identity.IsAuthenticated;
    }
}
