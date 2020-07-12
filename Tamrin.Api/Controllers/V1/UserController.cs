using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using Tamrin.Api.Models;
using Tamrin.Common.Exceptions;
using Tamrin.Data.Contracts;
using Tamrin.Services.Services.Contracts;
using Tamrin.WebFramework.Api;

namespace Tamrin.Api.Controllers.V1
{
    [ApiVersion("1")]
    public class UserController : BaseController
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, IJwtService jwtService, IMapper mapper, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region SignIn User

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public virtual async Task<IActionResult> SignIn([FromForm] SignInUserDto signInUser, CancellationToken cancellationToken)
        {
            if (!signInUser.Grant_Type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new AppException("OAuth Flow Is Not Password");

            var user = await _userRepository.GetUserByEmailAndPass(signInUser.UserName, signInUser.Password, cancellationToken);

            var jwt = _jwtService.Generate(user);

            return new JsonResult(jwt);
        }


        #endregion

        #region Register User

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public virtual async Task<IActionResult> RegisterUser(RegisterUserDto userDto, CancellationToken cancellationToken)
        {
            var user = userDto.ToEntity(_mapper);

            //var user = new User
            //{
            //    RoleId = 5,
            //    UserName = userDto.UserName.Trim(),
            //    Email = userDto.Email.ToLower().Trim(),
            //    GenderType = userDto.GenderType,
            //    FirstName = "تست نام",
            //    LastName = "تست فامیلی",
            //    AvatarName = "avatar.jpg",
            //    ActivationCode = Guid.NewGuid().ToString().Replace("-", ""),
            //    IsDeleted = false,
            //    CreateDateTime = DateTime.Now,
            //    AccessFailedCount = 0,
            //    EmailConfirmed = false,
            //    LockoutEnabled = false
            //};

            await _userRepository.AddAsync(user, userDto.Password, cancellationToken);

            return Ok();
        }

        #endregion

        #region Additional Method

        [HttpGet]
        public virtual async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var users = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "administrator")]
        public virtual async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        #endregion
    }


}
