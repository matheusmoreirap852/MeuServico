using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ProjetoServicoWork.Services.Contracts;
using ProjetoServicoWork.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Configurar a autenticação com cookies e OpenID Connect
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
        OnRedirectToAccessDenied = (context) =>
        {
            context.HttpContext.Response.Redirect(builder.Configuration["ServiceUri:IdentityServer"] + "/Account/AccessDenied");
            return Task.CompletedTask;
        }
    };
})
.AddOpenIdConnect("oidc", options =>
{
    // Aqui dentro do AddOpenIdConnect você pode configurar 'options'
    options.Events.OnRemoteFailure = context =>
    {
        context.Response.Redirect("/");
        context.HandleResponse();
        return Task.FromResult(0);
    };

    options.Authority = builder.Configuration["ServiceUri:IdentityServer"]; // URI do servidor de identidade
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClientId = "copaSesc"; // Id do cliente
    options.ClientSecret = builder.Configuration["Client:Secret"]; // Segredo do cliente
    options.ResponseType = "code";
    options.ClaimActions.MapJsonKey("role", "role", "role");
    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";
    options.Scope.Add("copaSesc"); // Escopo que está sendo solicitado
    options.SaveTokens = true;
    
    // Desabilitar a exigência de HTTPS para desenvolvimento
    options.RequireHttpsMetadata = false;  // Esta linha deve estar aqui
});

// Configurar a serialização JSON
builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
});

// Configurar o HttpClient para comunicação com a API de produtos
builder.Services.AddHttpClient<IServicoDados, ServicoDados>("ProductApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]); // URL base da API de produtos
    c.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
    c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
    c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-ProductApi");
});

// Registrar serviços
builder.Services.AddScoped<IServicoDados, ServicoDados>();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Ativar autenticação
app.UseAuthorization();  // Ativar autorização

// Definir rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
