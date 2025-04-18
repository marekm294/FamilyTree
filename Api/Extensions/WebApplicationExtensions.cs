﻿using Api.Middlewares;

namespace Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureWebApplication(this WebApplication webApplication)
    {
        webApplication.UseCors();

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
            .UseMiddleware<ErrorHandlingMiddlware>()
            .UseMiddleware<TenantMiddleware>();

        return webApplication;
    }
}