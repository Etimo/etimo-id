using Etimo.Id.Client;
using Etimo.Id.Constants;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Etimo.Id.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<IEtimoIdApplicationClient, EtimoIdApplicationClient>();
            builder.Services.AddTransient<IEtimoIdAuditLogClient, EtimoIdAuditLogClient>();
            builder.Services.AddTransient<IEtimoIdOAuthClient, EtimoIdOAuthClient>();
            builder.Services.AddTransient<IEtimoIdRoleClient, EtimoIdRoleClient>();
            builder.Services.AddTransient<IEtimoIdScopeClient, EtimoIdScopeClient>();
            builder.Services.AddTransient<IEtimoIdUserClient, EtimoIdUserClient>();

            builder.Services.AddHttpClient("Etimo.Id.Web", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddOidcAuthentication(
                options =>
                {
                    builder.Configuration.Bind("OidcProviderOptions", options.ProviderOptions);
                    options.ProviderOptions.ResponseType = ResponseTypes.Token;
                    options.ProviderOptions.ResponseMode = ResponseModes.Fragment;
                    options.ProviderOptions.DefaultScopes.Add("etimoid:read:application");
                });

            await builder.Build().RunAsync();
        }
    }
}
