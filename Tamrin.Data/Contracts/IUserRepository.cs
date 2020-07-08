using System.Threading;
using System.Threading.Tasks;
using Tamrin.Entities.User;

namespace Tamrin.Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAndPass(string email, string password, CancellationToken cancellationToken);

        Task AddAsync(User user,string password, CancellationToken cancellationToken);
    }
}