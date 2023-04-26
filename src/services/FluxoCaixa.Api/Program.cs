using System.Text.Json.Serialization;
using FluxoCaixa.Api.Configurations;
using FluxoCaixa.Api.Helpers;
using FluxoCaixa.Core.Converters;
using FluxoCaixa.Core.WebApi.Configurations;
using FluxoCaixa.Core.WebApi.Middlewares;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Infrastructure.CrossCutting.Mappers;
using FluxoCaixa.Infrastructure.Data.Configurations;
using FluxoCaixa.Infrastructure.Data.Context.Identidade;
using Identidade;
using Logging;
using MessageBus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração de logging com o serilog
builder.Logging.AddSerilog(new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.CreateLogger());
builder.Services.AddLoggerConfiguration();

// Configura as rotas no padrão de caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

// Adiciona configurações de validação
builder.Services.AddValidationConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger("FluxoCaixa.Api");

// Configuração de injeção de dependências
builder.Services.AddDependencyInjectionConfiguration();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MapDtoToEntity).Assembly);
builder.Services.AddAutoMapper(typeof(MapDtoToEvent).Assembly);

// Configuração de autenticação e autorização
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
builder.Services.Configure<IdentitySettings>(options => builder.Configuration.GetSection(nameof(IdentitySettings)).Bind(options));

builder.Services.AddIdentidadeConfiguration<IdentidadeContext>(identitySettings);

// Configuração do banco de dados referente ao contexto de Identidade
builder.Services.AddIdentidadeContextConfiguration();

// Configuração do banco de dados referente ao contexto de fluxo de caixa
builder.Services.AddFluxoCaixaContextConfiguration();

// Configuração do Message Bus com o RabbitMQ
builder.Services.AddMessageBusConfiguration();

// Configuração do health check
builder.Services.AddHealthChecksConfiguration();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHealthCheck();

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
