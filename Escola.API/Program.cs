using Escola.API.Filter;
using Escola.Application.Service;
using Escola.Application.Validator;
using Escola.Domain.Interface.Repository;
using Escola.Domain.Interface.Service;
using Escola.Infrastructure.Context;
using Escola.Infrastructure.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Escola",
        Description = "Api de escola"
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Cabeçalho de Autorização JWT esta usando o esquema de Beare"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthentication(e =>
{
    e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    e.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer( e =>
    {
        e.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))

        };
    });
builder.Services.AddDbContext<EscolaDbContext>(e => e.UseInMemoryDatabase(databaseName: "EscolaDB"));

builder.Services.AddControllers()
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AlunoValidator>(lifetime: ServiceLifetime.Transient));

//Repository
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<INotaRepository, NotaRepository>();

//Service
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<INotaService, NotaService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllersWithViews(options => options.Filters.Add(typeof(ValidationFilter)));

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
