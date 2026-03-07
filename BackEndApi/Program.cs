using BackEndApi.Context;
using BackEndApi.Dtos;
using BackEndApi.Dtos.Mappings;
using BackEndApi.Repositories;
using BackEndApi.Repositories.Base;
using BackEndApi.Repositories.IRepositories;
using BackEndApi.Service;
using BackEndApi.Service.IService;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text.Json.Serialization;
using BackEndApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Carregue as configurações do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meus Serviços", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"'Bearer' [space] seu token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
               },
               Scheme = "oauth2",
               Name = "Bearer",
               In= ParameterLocation.Header
            },
            new List<string> ()
         }
    });
});

// Adicione o serviço IDbConnection ao contêiner de injeção de dependência
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = configuration.GetConnectionString("Padrao");
    return new SqliteConnection(connectionString);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("Padrao"));
});




builder.Services.AddScoped<IMinhaInformacoesRepositorie, MinhaInformacoesRepositorie>();
builder.Services.AddScoped<IMinhaInformacoes, MinhaInformacoesService>();


builder.Services.AddScoped(typeof(IBaseRepository<>),
                           typeof(BaseRepository<>));


// ==========================
// SERVICES AUTO CRUD
// ==========================
builder.Services.AddScoped<IBaseService<CarroDto>, CarroService>();
builder.Services.AddScoped<IBaseService<ManutencaoDto>, ManutencaoService>();
builder.Services.AddScoped<IBaseService<DespesaGeralDto>, DespesaGeralService>();
builder.Services.AddScoped<IBaseService<LocacaoDto>, LocacaoService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddAuthentication("Bearer")
       .AddJwtBearer("Bearer", options =>
       {
           options.Authority =
             builder.Configuration["VShop.IdentityServer:ApplicationUrl"];

           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = false
           };
       });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "CopaSesc");
    });
});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();




