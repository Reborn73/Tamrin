using Tamrin.Entities.User;

namespace Tamrin.Services.Services.Contracts
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}