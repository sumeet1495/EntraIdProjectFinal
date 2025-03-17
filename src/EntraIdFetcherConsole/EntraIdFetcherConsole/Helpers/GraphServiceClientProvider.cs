using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Extensions.Configuration;

namespace EntraIdFetcherConsole.Helpers
{
    public class GraphServiceClientProvider
    {
        private readonly IConfiguration _configuration;

        public GraphServiceClientProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GraphServiceClient GetGraphServiceClient()
        {
            var tenantId = _configuration["AzureAd:TenantId"];
            var clientId = _configuration["AzureAd:ClientId"];
            var clientSecret = _configuration["AzureAd:ClientSecret"];

            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            return new GraphServiceClient(clientSecretCredential);
        }
    }
}