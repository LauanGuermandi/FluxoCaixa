using FluxoCaixa.Api.Configurations;
using FluxoCaixa.Api.Helpers;
using FluxoCaixa.Core.WebApi.Configurations;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Infrastructure.CrossCutting.Mappers;
using FluxoCaixa.Infrastructure.Data.Configurations;
using FluxoCaixa.Infrastructure.Data.Context.Identidade;
using Identidade.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura as rotas no padrão de caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger("FluxoCaixa.Api");

// Configuração de injeção de dependências
builder.Services.AddDependencyInjectionConfiguration();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MapDtoToEntity).Assembly);

// Configuração de autenticação e autorização
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
builder.Services.Configure<IdentitySettings>(options => builder.Configuration.GetSection(nameof(IdentitySettings)).Bind(options));

builder.Services.AddIdentidadeConfiguration<IdentidadeContext>(identitySettings);

// Add configuração do banco de dados referente ao contexto de Identidade
builder.Services.AddIdentidadeContextConfiguration();

// Add configuração do banco de dados referente ao contexto de fluxo de caixa
builder.Services.AddFluxoCaixaContextConfiguration();

var app = builder.Build();

// Executa as migrations de seed no start da aplicação
await DatabaseMigrationHelpers.RunMigrations(app);

if (app.Environment.IsDevelopment())
{
	app.UseSwaggerWithUI();
}

// Adiciona os midlewares para utilização do contexto de identidade
app.UseCustomAuthentication();

app.MapControllers();
app.Run();
