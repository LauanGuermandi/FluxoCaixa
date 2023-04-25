using FluxoCaixa.Api.Configurations;
using FluxoCaixa.Api.Helpers;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Infrastructure.Data.Configurations;
using Identidade.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura as rotas no padrão de caixa baixa
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyInjectionConfiguration();

// Configuração de autenticação e autorização
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
builder.Services.AddIdentidadeConfiguration(identitySettings);

// Add configuração do banco de dados referente ao contexto de Identidade
builder.Services.AddIdentidadeContextConfiguration();
builder.Services.AddCustomAuthentication();

var app = builder.Build();

// Executa as migrations de seed no start da aplicação
await DatabaseMigrationHelpers.RunMigrations(app);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adiciona os midlewares para utilização do contexto de identidade
app.UseCustomAuthentication();

app.MapControllers();
app.Run();
