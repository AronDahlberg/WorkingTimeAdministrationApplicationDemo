using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WTA_ClientApp.Providers;
using WTA_ClientApp.Services.Authentication;
using WTA_ClientApp.Services.Base;

namespace WTA_ClientApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7179") });

        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddScoped<ApiAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
            provider.GetRequiredService<ApiAuthenticationStateProvider>());
        builder.Services.AddAuthorizationCore();

        // NSwag client
        builder.Services.AddScoped<IClient, Client>();

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        await builder.Build().RunAsync();
    }
}
