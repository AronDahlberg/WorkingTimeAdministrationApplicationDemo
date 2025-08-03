using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WTA_ClientApp.Providers;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp.Services.Authentication
{
    public class AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : IAuthenticationService
    {
        private readonly IClient httpClient = httpClient;
        private readonly ILocalStorageService localStorage = localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider = authenticationStateProvider;

        public async Task<bool> AuthenticateAsync(UserLoginDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            await localStorage.SetItemAsync("accessToken", response.Token);

            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
