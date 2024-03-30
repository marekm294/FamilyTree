using Api.Extensions;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddApplicationServices(builder.Configuration);

        var app = builder.Build();
        app.ConfigureWebApplication();
        app.Run();
    }
}
