using FluxoCaixa.Api.Configurations;
using FluxoCaixa.Api.Helpers;
using FluxoCaixa.Core.WebApi.Configurations;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Infrastructure.CrossCutting.Mappers;
using FluxoCaixa.Infrastructure.Data.Configurations;
using FluxoCaixa.Infrastructure.Data.Context.Identidade;
using Identidade.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura as rotas no padr�o de caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger("FluxoCaixa.Api");

// Configura��o de inje��o de depend�ncias
builder.Services.AddDependencyInjectionConfiguration();

// Configura��o do AutoMapper
builder.Services.AddAutoMapper(typeof(MapDtoToEntity).Assembly);

// Configura��o de autentica��o e autoriza��o
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
builder.Services.Configure<IdentitySettings>(options => builder.Configuration.GetSection(nameof(IdentitySettings)).Bind(options));

builder.Services.AddIdentidadeConfiguration<IdentidadeContext>(identitySettings);

// Add configura��o do banco de dados referente ao contexto de Identidade
builder.Services.AddIdentidadeContextConfiguration();

// Add configura��o do banco de dados referente ao contexto de fluxo de caixa
builder.Services.AddFluxoCaixaContextConfiguration();

var app = builder.Build();

// Executa as migrations de seed no start da aplica��o
await DatabaseMigrationHelpers.RunMigrations(app);

if (app.Environment.IsDevelopment())
{
	app.UseSwaggerWithUI();
}

// Adiciona os midlewares para utiliza��o do contexto de identidade
app.UseCustomAuthentication();

app.MapControllers();
app.Run();
