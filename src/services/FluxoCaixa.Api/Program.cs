using FluxoCaixa.Api.Configurations;
using FluxoCaixa.Api.Helpers;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Infrastructure.Data.Configurations;
using Identidade.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura as rotas no padr�o de caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyInjectionConfiguration();

// Configura��o de autentica��o e autoriza��o
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
builder.Services.AddIdentidadeConfiguration(identitySettings);

// Add configura��o do banco de dados referente ao contexto de Identidade
builder.Services.AddIdentidadeContextConfiguration();
builder.Services.AddCustomAuthentication();

var app = builder.Build();

// Executa as migrations de seed no start da aplica��o
await DatabaseMigrationHelpers.RunMigrations(app);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adiciona os midlewares para utiliza��o do contexto de identidade
app.UseCustomAuthentication();

app.MapControllers();
app.Run();
