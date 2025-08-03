using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UserLoginDto loginModel);
        public Task Logout();
    }
}
