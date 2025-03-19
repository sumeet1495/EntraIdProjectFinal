using EntraIdFetcher.Interfaces;
using EntraIdFetcher.Services;
using EntraIdFetcher.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EntraIdFetcherConsole
{
    class Program
    {
        private readonly IUserService _userService;
        private readonly IGraphAuthProvider _graphAuthProvider;
        private readonly IConfiguration _configuration;

        // Constructor invocation and initialisation
        public Program(IUserService userService, IGraphAuthProvider graphAuthProvider, IConfiguration configuration)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }
            if (graphAuthProvider == null)
            {
                throw new ArgumentNullException(nameof(graphAuthProvider));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _userService = userService;
            _graphAuthProvider = graphAuthProvider;
            _configuration = configuration;
        }
        static async Task Main(string[] args)
        {
            //  configuration loading
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // dependencies instantiation via interface
            IGraphAuthProvider graphAuthProvider = new GraphServiceClientProvider(config);
            var graphClient = graphAuthProvider.GetGraphServiceClient();
            IUserService userService = new UserService(graphClient);

            // passing dependencies via Constructor Injection
            var program = new Program(userService, graphAuthProvider, config);
            await program.RunAsync();
        }

        public async Task RunAsync()
        {
            var objectId = _configuration["Graph:UserObjectId"];
            var userDetails = await _userService.GetUserDetailsByIdAsync(objectId);

            Console.WriteLine("User Details:");
            Console.WriteLine($"Display Name: {userDetails.DisplayName}");
            Console.WriteLine($"First Name: {userDetails.FirstName}");
            Console.WriteLine($"Last Name: {userDetails.LastName}");
            Console.WriteLine($"User Principal Name: {userDetails.UserPrincipalName}");
            Console.WriteLine($"Object ID: {userDetails.ObjectId}");
            Console.WriteLine($"Office Location: {userDetails.OfficeLocation}");
            Console.WriteLine($"Mobile Phone: {userDetails.MobilePhone}");
        }
    }
}
