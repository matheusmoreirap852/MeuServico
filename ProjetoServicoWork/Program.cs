using MeuServico.Application.Mappings;
using MeuServico.Infrastructure;
using MeuServico.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjetoServicoWork.Services;
using ProjetoServicoWork.Services.Contracts;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();


// 🔹 INFRASTRUCTURE (igual API)
builder.Services.AddInfrastructure(builder.Configuration);


// 🔹 AUTOMAPPER
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(MappingProfile).Assembly
);


// 🔹 CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});


// 🔹 AUTHENTICATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies", c =>
{
    c.ExpireTimeSpan = TimeSpan.FromMinutes(10);

    c.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToAccessDenied = context =>
        {
            context.HttpContext.Response.Redirect(
                builder.Configuration["ServiceUri:IdentityServer"] + "/Account/AccessDenied"
            );

            return Task.CompletedTask;
        }
    };
})
.AddOpenIdConnect("oidc", options =>
{
    options.Events.OnRemoteFailure = context =>
    {
        context.Response.Redirect("/");
        context.HandleResponse();
        return Task.FromResult(0);
    };

    options.Authority = builder.Configuration["ServiceUri:IdentityServer"];
    options.GetClaimsFromUserInfoEndpoint = true;

    options.ClientId = "vshop";
    options.ClientSecret = builder.Configuration["Client:Secret"];

    options.ResponseType = "code";

    options.ClaimActions.MapJsonKey("role", "role", "role");
    options.ClaimActions.MapJsonKey("sub", "sub", "sub");

    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";

    options.Scope.Add("vshop");

    options.SaveTokens = true;
});


// 🔹 JSON OPTIONS
builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
});


// 🔹 HTTP CLIENT (se ainda usar)
builder.Services.AddHttpClient<IServicoDados, ServicoDados>("ProductApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
});

builder.Services.AddScoped<IServicoDados, ServicoDados>();



var app = builder.Build();


// 🔹 AUTO MIGRATION (igual API)
using (var scope = app.Services.CreateScope())
{
    var dbContext =
        scope.ServiceProvider.GetRequiredService<AppDbContext>();

    dbContext.Database.Migrate();
}


// 🔹 PIPELINE

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();


// 🔹 ROUTE PADRÃO
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();