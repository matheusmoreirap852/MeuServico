using MeuServico.Application.Mappings;
using MeuServico.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Clean Architecture
builder.Services.AddInfrastructure(builder.Configuration);

// ✅ AutoMapper (.NET 10 / v16)
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(MappingProfile).Assembly
);

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();