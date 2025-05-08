using FaiscaSync.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FaiscaSync.Services;
using Microsoft.EntityFrameworkCore;
using FaiscaSync;
using FaiscaSync.Services;
using FaiscaSync.Services.Interface;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

// Configuração do JWT
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
        )
    };
});

builder.Services.AddAuthentication();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
   
builder.Services.AddDbContext<FsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // mantém log na consola
builder.Logging.AddFile(builder.Configuration.GetSection("Logging:File:Path").Value);


builder.Host.UseSerilog();

builder.Services.AddScoped<IAquisicaoService,AquisicaoService>();
builder.Services.AddScoped<ICargoService,CargoService>();
builder.Services.AddScoped<IClienteService,ClienteService>();
builder.Services.AddScoped<ICoberturaGarantiaService,CoberturaGarantiaService>();
builder.Services.AddScoped<IContatoService,ContatoService>();
builder.Services.AddScoped<ICPostalService,CPostalService>();
builder.Services.AddScoped<IEstadoGarantiaService,EstadoGarantiaService>();
builder.Services.AddScoped<IEstadoManutencaoService,EstadoManutencaoService>();
builder.Services.AddScoped<IEstadoVeiucloService,EstadoVeiculoService>();
builder.Services.AddScoped<IFaturaService,FaturaService>();
builder.Services.AddScoped<IFuncionarioService,FuncionarioService>();
builder.Services.AddScoped<IGarantiaService,GarantiaService>();
builder.Services.AddScoped<IHistoricoEstadoVeiculoService,HistoricoEstadoVeiculoService>();
builder.Services.AddScoped<ILoginService,LoginService>();
builder.Services.AddScoped<IManutencaoService,ManutencaoService>();
builder.Services.AddScoped<IMarcaService,MarcaService>();
builder.Services.AddScoped<IModeloService,ModeloService>();
builder.Services.AddScoped<IMoradaService,MoradaService>();
builder.Services.AddScoped<IMotorService,MotorService>();
builder.Services.AddScoped<IPagamentoService,PagamentoService>();
builder.Services.AddScoped<IPosVendaService,PosVendaService>();
builder.Services.AddScoped<ITipoContatoService,TipoContatoService>();
builder.Services.AddScoped<ITipoPagamentoService, TipoPagamentoService>();
builder.Services.AddScoped<ITipoVeiculoService,TipoVeiculoService>();
builder.Services.AddScoped<IVeiculoService,VeiculoService>();
builder.Services.AddScoped<IVendaService,VendaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200")  // porta padrão do Angular
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAngularApp");
app.Run();

public partial class Program { }


