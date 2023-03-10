using Projecto_Final.Models;

namespace Projecto_Final.Helpers
{
    public interface IJwtTokenAuth
    {
        string GenerateJwtToken(User user);
    }
}
