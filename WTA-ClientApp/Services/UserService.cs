using Blazored.LocalStorage;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services
{
    public class UserService(IClient client, ILocalStorageService localStorage) : BaseHttpService(localStorage, client), IUserService
    {
        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            await GetBearerToken();
            try
            {
                return await client.GetUserAsync(userId);
            }
            catch (ApiException aex)
            {
                return null;
            }
        }
    }
}
