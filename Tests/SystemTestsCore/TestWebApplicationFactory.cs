using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SystemTestsCore;

public class TestWebApplicationFactory<TAssembly, TProgram> : WebApplicationFactory<TProgram>
    where TAssembly : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private const string TEST_ENVIROMENT = "Test";

    private static string TEST_APPSETTING_FILE_NAME
        = $"{AppDomain.CurrentDomain.BaseDirectory}appsettings.{TEST_ENVIROMENT}.json";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder
            .ConfigureHostConfiguration(configurationBuilder =>
            {
                configurationBuilder
                    .AddJsonFile(TEST_APPSETTING_FILE_NAME, false)
                    .AddUserSecrets(typeof(TAssembly).Assembly);
            });

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment(TEST_ENVIROMENT);
    }
}