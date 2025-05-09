using FaiscaSync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FaiscaSync.Services;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// JWT CONFIG
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
        RoleClaimType = ClaimTypes.Role
    };
});
builder.Services.AddAuthentication();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FaiscaSync", Version = "v1" });

        // Configurar autenticação JWT no Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Coloca o Token:"
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
                }
            },
            Array.Empty<string>()
        }
    });
    });

builder.Services.AddScoped<FaturaServices>();
builder.Services.AddScoped<ManutencaoServices>();
builder.Services.AddScoped<CargoServices>();
builder.Services.AddScoped<EstadoGarantiaServices>();
builder.Services.AddScoped<EstadoVeiculoServices>();
builder.Services.AddScoped<EstadoManutencaoServices>();
builder.Services.AddScoped<CpostalServices>();
builder.Services.AddScoped<MoradaServices>();
builder.Services.AddScoped<MotorServices>();
builder.Services.AddScoped<ModeloServices>();
builder.Services.AddScoped<TipoVeiculoServices>();
builder.Services.AddScoped<GarantiaServices>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VeiculoServices>();
builder.Services.AddScoped<VendaServices>();
builder.Services.AddScoped<FuncionarioServices>();
builder.Services.AddAuthorization();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:53458") // Porta do seu front-end
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Configuração do DbContext
builder.Services.AddDbContext<FsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("fs")));

var app = builder.Build();

app.UseAuthentication();

// Adiciona o middleware para CORS
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
