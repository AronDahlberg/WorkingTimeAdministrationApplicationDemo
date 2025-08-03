using Blazored.LocalStorage;

namespace WTA_ClientApp.Services.Base
{
    public class BaseHttpService(ILocalStorageService localStorage, IClient client)
    {
        private readonly ILocalStorageService localStorage = localStorage;
        private readonly IClient client = client;

        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}
