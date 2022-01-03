using System.Security.Claims;
using HotChocolate.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ProsperousUniverse.API;
using ProsperousUniverse.API.DataLoaders;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);

bool useAuth = builder.Configuration.GetValue<bool>("UseAuth");
if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("Redis")))
{
    builder.Services.AddStackExchangeRedisCache(x
        => x.ConfigurationOptions = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")));
}

var v = builder.Services
    .AddHealthChecks()
    .AddCheck<PUConnectionCheck>("PUSocketConnection")
    .Services
    .AddOptions<AuthConfig>().Bind(builder.Configuration.GetSection("Auth"))
    .Services
    .AddHttpClient<AuthService>()
    .Services
    .AddFusionCacheSystemTextJsonSerializer()
    .AddSingleton<SocketWorker>()
    .AddSingleton<CountryRegistry>()
    .AddSingleton<MaterialCategories>()
    .AddHostedService(x => x.GetRequiredService<SocketWorker>())
    .AddSingleton<IServerInterface>(x => x.GetRequiredService<SocketWorker>())
    .AddFusionCache(x =>
    {
        x.DefaultEntryOptions = new FusionCacheEntryOptions()
        {
            FailSafeThrottleDuration = TimeSpan.FromSeconds(10),
            Duration = TimeSpan.FromMinutes(15),
            JitterMaxDuration = TimeSpan.FromMinutes(3),
            AllowTimedOutFactoryBackgroundCompletion = true,
            AllowBackgroundDistributedCacheOperations = true,
        };
    })
    .AddSingleton<DataCache>()

    .AddGraphQLServer()
    .AddGlobalObjectIdentification()
    .AddQueryType<Query>()
    .AddInMemorySubscriptions()
    .AddDataLoader<UserByIdDataLoader>()
    .AddDataLoader<CompanyByIdDataLoader>()
    .AddDataLoader<CountryByIdDataLoader>()
    .AddDataLoader<CorporationByIdDataLoader>()
    .AddDataLoader<SystemByIdDataLoader>()
    .AddDataLoader<PlanetByIdDataLoader>()
    .AddDataLoader<MaterialByIdDataLoader>()
    .AddDataLoader<MaterialByTickerDataLoader>()
    .AddDataLoader<MaterialCategoryByIdDataLoader>()
    .AddDataLoader<ReactorByIdDataLoader>()
    .AddDataLoader<PopulationByIdDataLoader>()
    .AddInterfaceType<IWorldEntityDTO>()
    .AddType<PlanetDTO>()
    .AddType<SystemDTO>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .UseDefaultPipeline();

if (useAuth)
{
    v.AddAuthorization().Services.AddAuthorization(options =>
    {
        void AddAuth0(string scope)
        {
            options.AddPolicy(scope,
                policy => policy.Requirements.Add(new HasScopeRequirement(scope, "https://kaij.eu.auth0.com/")));
        }

        AddAuth0("read:pu_building_recipe");
        AddAuth0("read:pu_build_option");
        AddAuth0("read:pu_company");
        AddAuth0("read:pu_corporation");
        AddAuth0("read:pu_corporation_shareholder");
        AddAuth0("read:pu_country");
        AddAuth0("read:pu_currency");
        AddAuth0("read:pu_currency_amount");
        AddAuth0("read:pu_material_category");
        AddAuth0("read:pu_material");
        AddAuth0("read:pu_material_infrastructure_usage");
        AddAuth0("read:pu_material_quantity");
        AddAuth0("read:pu_orbit");
        AddAuth0("read:pu_planet");
        AddAuth0("read:planet_local_rules");
        AddAuth0("read:pu_planet_resource");
        AddAuth0("read:pu_population");
        AddAuth0("read:pu_production_fee");
        AddAuth0("read:pu_rating_report");
        AddAuth0("read:pu_reactor");
        AddAuth0("read:pu_recipe");
        AddAuth0("read:pu_system");
        AddAuth0("read:pu_user");
        AddAuth0("read:pu_workforce_capacity");
        AddAuth0("read:pu_address");
        AddAuth0("read:pu_present_user");

    }).AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Authority = "https://kaij.eu.auth0.com/";
        options.Audience = "https://api.pu.kaij.tech/graphql";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
        };
        options.SaveToken = true;
    }).Services
        .AddSingleton<IAuthorizationHandler, HasScopeHandler>();
}

var app = builder.Build();

if (useAuth)
    app.UseAuthentication();

app.UseHealthChecks(new PathString("/health"));

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", (async context =>
{
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Check /graphql for the API or /health for health information.\n");
    if (useAuth)
    {
        var authenticateInfo = await context.AuthenticateAsync();
        if (authenticateInfo.Principal?.Identity is not null)
        {
            await context.Response.WriteAsync(
                $"User logged in as {authenticateInfo.Principal.Identity.Name} via {authenticateInfo.Principal.Identity.AuthenticationType}.\n");
        }

        var token = authenticateInfo.Ticket?.Properties.Items[".Token.access_token"];
        if (token is not null)
        {
            await context.Response.WriteAsync(token);
        }
    }
    else
    {
        await context.Response.WriteAsync("Not using Auth");
    }
}));

app.MapGraphQL();

app.Run();
