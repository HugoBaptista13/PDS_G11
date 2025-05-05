using FaiscaSync.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FaiscaSync.Services;
using Microsoft.EntityFrameworkCore;
using FaiscaSync;

var builder = WebApplication.CreateBuilder(args);

// Configuração do JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = key
        };
    });


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAquisicaoService,AquisicaoService>();
builder.Services.AddScoped<ICargoService,CargoService>();
builder.Services.AddScoped<IClienteService,ClienteService>();
builder.Services.AddScoped<ICoberturaGarantiaService,CoberturaGarantiaService>();
builder.Services.AddScoped<IContatoService,ContatoService>();
builder.Services.AddScoped<ICPostalService,CPostalService>();
builder.Services.AddScoped<IEstadoGarantiaService,EstadoGarantiaService>();
builder.Services.AddScoped<IEstadoManutencaoService,EstadoManutencaoService>();
builder.Services.AddScoped<IEstadoVeiculoService,EstadoVeiculoService>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication(); // ← JWT: identifica o utilizador
app.UseAuthorization();

app.MapControllers();

app.Run();

