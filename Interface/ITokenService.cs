using stock_fincance_api.Models;

namespace stock_fincance_api.Interface
{
    public interface ITokenService
    {
        string CreateTokenService(AppUser user);
    }
}
