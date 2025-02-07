using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(",") ?? Array.Empty<string>();


// add cors
// Access-Control-Allow-Credentials : true
// Access-Control-Allow-Origin: http:localhost:portnumber
// * can't be used when we are using allowcreditionals 
// builder.AllowAnyOrigin().AllowCredentials() -- not allowed security measure by cors.
builder.Services.
        AddCors(options => {
            options.AddPolicy("AllowAnyOrigin", 
                                builder => {
                                    builder.WithOrigins(allowedOrigins).AllowCredentials();
                                    builder.WithOrigins("https://*.domainname.com");
                                    builder.SetIsOriginAllowedToAllowWildcardSubdomains();
                             });
            options.AddPolicy("PublicApi", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("content-type"));
        });

// builder.WithOrigins(allowedOrigins).SetPreFlightMaxAge(TimeSpan.FormMinutes(1)); // with cache time
// builder.WithExposedHeaders("PageNo", "PageSize", "PageCount", "PageTotalRecords"); // to make the headers available.

builder.Services.
        AddCors(options => options.AddPolicy("TestSetup", 
        builder => builder.WithOrigins(allowedOrigins)));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.Logger.LogInformation("The app started");

app.Logger.LogInformation(string.Join(",", allowedOrigins));


// allow cors
app.UseCors("TestSetup");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// [EnableCors("PublicApi")]
app.MapGet("/weatherforecast", (HttpContext context) =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    // exposing custom headers.
    // HttpContext.Response.Headers.Add(PageNo, pageNo.ToString());
    // HttpContext.Response.Headers.Add(PageSize, pageSize.ToString());
    // HttpContext.Response.Headers.Add(PageCount, pageCount.ToString());
    // HttpContext.Response.Headers.Add(PageTotalRecords, totalRecords.ToString());

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

