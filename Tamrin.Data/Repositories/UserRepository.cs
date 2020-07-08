using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamrin.Common;
using Tamrin.Common.Exceptions;
using Tamrin.Common.Utilities;
using Tamrin.Data.Contracts;
using Tamrin.Entities.User;

namespace Tamrin.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmailAndPass(string email, string password, CancellationToken cancellationToken)
        {
            var user = await Table.SingleOrDefaultAsync(u => u.Email == email.Trim().ToLower(), cancellationToken: cancellationToken);
            if (user == null)
                throw new NotFoundException("کاربری با این ایمیل یافت نشد");

            if (!user.EmailConfirmed)
                throw new NotFoundException("حساب کاربری شما غیر فعال است.لطفا از طریق لینکی که برای ایمیل شما ارسال شده است، حساب کاربری خود را فعال کنید");

            if (user.IsDeleted)
                throw new NotFoundException("حساب کاربری شما توسط مدیریت غیر فعال شده است، لطفا از طریق فرم تماس با ما پیگیری کنید.");

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                var bannedTimeSpan = user.LockoutEnd - DateTime.Now;
                var min = bannedTimeSpan.Value.Minutes > 0 ? bannedTimeSpan.Value.Minutes : 1;
                throw new NotFoundException($"حساب کاربری شما بدلیل اشتباه در ورود کلمه عبور تا { min } دقیقه دیگر غیر فعال است.");
            }

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            if (user.PasswordHash != passwordHash && user.AccessFailedCount == 4)
            {
                user.AccessFailedCount = 0;
                user.LockoutEnd = DateTime.Now.AddMinutes(30);
                await base.UpdateAsync(user, cancellationToken);
                throw new NotFoundException($"حساب کاربری شما به مدت ۳۰ دقیقه غیر فعال شده است");
            }

            if (user.PasswordHash != passwordHash)
            {
                user.AccessFailedCount++;
                await base.UpdateAsync(user, cancellationToken);
                throw new NotFoundException($"کلمه ی عبور اشتباه است در صورتی که {5 - user.AccessFailedCount} بار دیگر اشتباه وارد کنید، حساب شما به مدت ۳۰ دقیقه غیر فعال میشود.");
            }

            await LoadReferenceAsync(user, u => u.Role, cancellationToken);

            return user;
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            if (await TableNoTracking.AnyAsync(u => u.UserName == user.UserName, cancellationToken: cancellationToken))
                throw new BadRequestException("نام کاربری تکراری است");

            if (await TableNoTracking.AnyAsync(u => u.Email == user.Email, cancellationToken: cancellationToken))
                throw new BadRequestException("ایمیل تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            user.SecurityStamp = Guid.NewGuid();
            await base.AddAsync(user, cancellationToken);
        }
    }
}
