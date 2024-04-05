using ProjetoServicoWork.Auth.Configuration;
using ProjetoServicoWork.Auth.Data;
using Duende.IdentityServer.Services;
using Auth.Data;
using Auth.Models;
using Auth.SeedDatabase;
using Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var mySqlConnection = builder.Configuration.GetConnectionString("padrao");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(mySqlConnection);
}, ServiceLifetime.Scoped);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

//configurações dos serviços do IdentityServer
var builderIdentityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(
                       IdentityConfiguration.IdentityResources)
                       .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                       .AddInMemoryClients(IdentityConfiguration.Clients)
                       .AddAspNetIdentity<ApplicationUser>();

builderIdentityServer.AddDeveloperSigningCredential();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IDatabaseSeedInitializer, DatabaseIdentityServerInitializer>();
builder.Services.AddScoped<IProfileService, ProfileAppService>();
builder.Services.AddScoped<IEmailSender, AuthMessageSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

SeedDatabaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabaseIdentityServer(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var initRolesUsers = serviceScope.ServiceProvider
                               .GetService<IDatabaseSeedInitializer>();

        //initRolesUsers.InitializeSeedRoles();
        //initRolesUsers.InitializeSeedUsers();
    }
}