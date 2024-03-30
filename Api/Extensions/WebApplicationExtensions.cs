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

        return webApplication;
    }
}