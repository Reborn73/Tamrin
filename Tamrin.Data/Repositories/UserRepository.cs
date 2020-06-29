using System;
using System.Collections.Generic;
using System.Text;
using Tamrin.Data.Contracts;
using Tamrin.Entities.User;

namespace Tamrin.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
