using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SitecoreSendProxy.Extensions
{
    public static class HttpClientExtensions
    {
        private static string GetClientUrl(this IServiceProvider provider, string clientName)
        {
            var config = provider.GetService<IConfiguration>();
            var name = $"HttpClient:{clientName}";
            return config?.GetSection(name).Value;
        }

        public static void AddClient(this IServiceCollection services, string clientName)
        {
            services.AddHttpClient(clientName,
                (provider, client) =>
                {
                    var url = provider.GetClientUrl(clientName);
                    if (url != null)
                    {
                        client.BaseAddress = new Uri(url);
                    }
                });
        }
    }
}