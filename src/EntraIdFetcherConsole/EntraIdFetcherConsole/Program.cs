using EntraIdFetcher.Interfaces;
using EntraIdFetcher.Services;
using EntraIdFetcherConsole.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    private readonly IUserService _userService;

    public Program(IUserService userService)
    {
        _userService = userService;
    }

    public async Task RunAsync(string objectId)
    {
        var userDetails = await _userService.GetUserDetailsByIdAsync(objectId);

        Console.WriteLine($"Display Name: {userDetails.DisplayName}");
        Console.WriteLine($"First Name: {userDetails.FirstName}");
        Console.WriteLine($"Last Name: {userDetails.LastName}");
        Console.WriteLine($"User Principal Name: {userDetails.UserPrincipalName}");
        Console.WriteLine($"Object ID: {userDetails.ObjectId}");
        Console.WriteLine($"Office Location: {userDetails.OfficeLocation}");
        Console.WriteLine($"Mobile Phone: {userDetails.MobilePhone}");
    }

    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var graphProvider = new GraphServiceClientProvider(config);
        var graphClient = graphProvider.GetGraphServiceClient();

        IUserService userService = new UserService(graphClient);
        var objectId = config["Graph:UserObjectId"];

        var program = new Program(userService);
        await program.RunAsync(objectId);
    }
}
