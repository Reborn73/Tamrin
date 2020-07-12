using Tamrin.Entities.User;
using Tamrin.Services.Services.Implementation;

namespace Tamrin.Services.Services.Contracts
{
    public interface IJwtService
    {
        AccessToken Generate(User user);
    }
}