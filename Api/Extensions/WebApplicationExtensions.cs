using Api.Middlewares;

namespace Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureWebApplication(this WebApplication webApplication)
    {
        // Configure the HTTP request pipeline.
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        webApplication.UseHttpsRedirection();

        webApplication.UseAuthorization();

        webApplication.MapControllers();

        webApplication.UseMiddlewares();

        return webApplication;
    }

    public static WebApplication UseMiddlewares(this WebApplication webApplication)
    {
        webApplication
            .UseMiddleware<ErrorHandlingMiddlware>();

        return webApplication;
    }
}