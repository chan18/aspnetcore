
# CORS configuration


## Configuring CORS in ASP.NET core application 

```C#
services.AddCors(options=> options.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin()));
```

## Restricting CORS in ASP.NET core application 

```C#

// startup constructor
public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

// startup var.
public IConfiguration Configuration { get; }

// AT ConfigureServices(IServiceCollection services)

var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
            
services.AddCors(options => options.AddPolicy("GlobomanticsInternal", 
                    builder => builder.WithOrigins(allowedOrigins)));


```

## Defining multiple CORS policies 

```c#

// list of actions List<Actions> so it is possible to do so.
var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
            services.AddCors(options =>
            {
                options.AddPolicy("GlobomanticsInternal", builder => builder.WithOrigins(allowedOrigins));
                options.AddPolicy("PublicApi", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("Content-Type"));
            });
```

## Allowing credentials for CORS requests 
```c#
// AllowCredentials()
 var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
            services.AddCors(options =>
            {
                options.AddPolicy("GlobomanticsInternal", builder => builder.AllowAnyOrigin().AllowCredentials());
                options.AddPolicy("PublicApi", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("Content-Type"));
            });
```

## Debugging CORS issues

```c#

```


## Exposing custom headers in CORS

```c#
var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
            services.AddCors(options =>
            {
                options.AddPolicy("GlobomanticsInternal", builder => {
                    builder.WithOrigins(allowedOrigins);
                    builder.WithExposedHeaders("PageNo", "PageSize", "PageCount", "PageTotalRecords");
                });
                options.AddPolicy("PublicApi", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("Content-Type"));
            });   
```

## Configuring wildcard subdomains and runtime validation 

```c#
services.AddCors(options =>
            {
                options.AddPolicy("GlobomanticsInternal", builder => {
                    //don't use both at once

                    //wildcard subdomains
                    builder.WithOrigins("http://*.globomanticsshop.com");
                    builder.SetIsOriginAllowedToAllowWildcardSubdomains();

                    //runtime validation
                    //builder.SetIsOriginAllowed(IsOriginAllowed);
                });
            });
```
