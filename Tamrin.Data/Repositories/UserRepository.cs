using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            return base.AddAsync(user, cancellationToken);
        }
    }
}
