using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamrin.Api.Models;
using Tamrin.Data.Contracts;
using Tamrin.Entities.User;
using Tamrin.WebFramework.Filters;

namespace Tamrin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var users = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }


        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto userDto, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                GenderType = userDto.GenderType,
                RoleId = 1,
                FirstName = "تست نام",
                LastName = "تست فامیلی",
                AvatarName = "avatar.jpg",
                ActiveCode = Guid.NewGuid().ToString().Replace("-", ""),
                IsActive = true,
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
            };

            await _userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return Ok(user);
        }

    }


}
