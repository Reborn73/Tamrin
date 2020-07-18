using System;
using System.Linq;
using Tamrin.Common.Utilities;
using Tamrin.Data.Contracts;
using Tamrin.Entities.User;

namespace Tamrin.Services.DataInitializer
{
    public class UserDataInitializer : IDataInitializer
    {
        private readonly IRepository<User> _userRepository;

        public UserDataInitializer(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void InitializeData()
        {
            var administratorUser = new User
            {
                RoleId = 1,
                FirstName = "رضا",
                LastName = "نیستانی",
                UserName = "reborn",
                Email = "reza.neyestani94@gmail.com",
                EmailConfirmed = true,
                PasswordHash = SecurityHelper.GetSha256Hash("123456"),
                AvatarName = "avatar.jpg",
                ActivationCode = Guid.NewGuid().ToString().Replace("-", ""),
                AccessFailedCount = 0,
                GenderType = GenderType.Male,
                LockoutEnabled = false,
                LockoutEnd = null,
                SecurityStamp = Guid.NewGuid(),
                IsDeleted = false,
            };

            if (!_userRepository.TableNoTracking.Any(u => u.RoleId == 1))
            {
                _userRepository.Add(administratorUser);
            }
        }
    }
}
