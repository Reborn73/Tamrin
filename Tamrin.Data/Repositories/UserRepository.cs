using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tamrin.Common.Exceptions;
using Tamrin.Common.Utilities;
using Tamrin.Data.Contracts;
using Tamrin.Entities.User;

namespace Tamrin.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(u => u.PasswordHash == passwordHash && u.UserName == username).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            if (await TableNoTracking.AnyAsync(u => u.UserName == user.UserName, cancellationToken: cancellationToken))
                throw new BadRequestException("نام کاربری تکراری است");

            if (await TableNoTracking.AnyAsync(u => u.Email == user.Email, cancellationToken: cancellationToken))
                throw new BadRequestException("ایمیل تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }
    }
}
