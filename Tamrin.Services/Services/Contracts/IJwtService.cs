using System.Threading.Tasks;
using Tamrin.Entities.User;

namespace Tamrin.Services.Services.Contracts
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}