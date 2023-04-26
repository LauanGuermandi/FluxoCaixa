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

// Configura��o de logging com o serilog
builder.Logging.AddSerilog(new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.CreateLogger());
builder.Services.AddLoggerConfiguration();

// Configura as rotas no padr�o de caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

// Adiciona configura��es de valida��o
builder.Services.AddValidationConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger("FluxoCaixa.Api");

// Configura��o de inje��o de depend�ncias
builder.Services.AddDependencyInjectionConfiguration();

// Configura��o do AutoMapper
builder.Services.AddAutoMapper(typeof(MapDtoToEntity).Assembly);
builder.Services.AddAutoMapper(typeof(MapDtoToEvent).Assembly);

// Configura��o de autentica��o e autoriza��o
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
builder.Services.Configure<IdentitySettings>(options => builder.Configuration.GetSection(nameof(IdentitySettings)).Bind(options));

builder.Services.AddIdentidadeConfiguration<IdentidadeContext>(identitySettings);

// Configura��o do banco de dados referente ao contexto de Identidade
builder.Services.AddIdentidadeContextConfiguration();

// Configura��o do banco de dados referente ao contexto de fluxo de caixa
builder.Services.AddFluxoCaixaContextConfiguration();

// Configura��o do Message Bus com o RabbitMQ
builder.Services.AddMessageBusConfiguration();

// Configura��o do health check
builder.Services.AddHealthChecksConfiguration();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHealthCheck();

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
