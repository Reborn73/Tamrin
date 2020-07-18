using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Tamrin.Data.Contracts;
using Tamrin.Entities.User;

namespace Tamrin.Services.DataInitializer
{
    public class RoleDataInitializer : IDataInitializer
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleDataInitializer(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void InitializeData()
        {
            var administratorRole = new Role
            {
                Id = 1,
                Name = "administrator",
                Title = "مدیر کل سایت",
                IsHide = true,
                IsDeleted = false
            };

            var adminRole = new Role
            {
                Id = 2,
                Name = "admin",
                Title = "مدیر سایت",
                IsHide = false,
                IsDeleted = false
            };

            var teacherRole = new Role
            {
                Id = 3,
                Name = "teacher",
                Title = "استاد",
                IsHide = false,
                IsDeleted = false
            };

            var writerRole = new Role
            {
                Id = 4,
                Name = "writer",
                Title = "نویسنده",
                IsHide = false,
                IsDeleted = false
            };

            var userRole = new Role
            {
                Id = 5,
                Name = "user",
                Title = "کاربر عادی",
                IsHide = false,
                IsDeleted = false
            };

            if (!_roleRepository.TableNoTracking.Any(p => p.Name == administratorRole.Name))
            {
                _roleRepository.Add(administratorRole);
            }

            if (!_roleRepository.TableNoTracking.Any(p => p.Name == adminRole.Name))
            {
                _roleRepository.Add(adminRole);
            }

            if (!_roleRepository.TableNoTracking.Any(p => p.Name == teacherRole.Name))
            {
                _roleRepository.Add(teacherRole);
            }

            if (!_roleRepository.TableNoTracking.Any(p => p.Name == writerRole.Name))
            {
                _roleRepository.Add(writerRole);
            }

            if (!_roleRepository.TableNoTracking.Any(p => p.Name == userRole.Name))
            {
                _roleRepository.Add(userRole);
            }

        }
    }
}
