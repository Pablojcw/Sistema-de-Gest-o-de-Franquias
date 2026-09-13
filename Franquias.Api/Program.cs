using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Franquias.Application.Interfaces.Repositories;
using Franquias.Application.Services;
using Franquias.Application.Services.Interfaces;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Franquias.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IFranquiaRepository, FranquiaRepository>();
builder.Services.AddScoped<IFranquiaService, FranquiaService>();

builder.Services.AddScoped<IFranqueadoRepository, FranqueadoRepository>();
builder.Services.AddScoped<IFranqueadoService, FranqueadoService>();

builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();

builder.Services.AddScoped<IFornecedorProdutoRepository, FornecedorProdutoRepository>();
builder.Services.AddScoped<IFornecedorProdutoService, FornecedorProdutoService>();

builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();
builder.Services.AddScoped<IUnidadeService, UnidadeService>();

builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();

builder.Services.AddScoped<IAtualizacaoChamadoRepository, AtualizacaoChamadoRepository>();
builder.Services.AddScoped<IAtualizacaoChamadoService, AtualizacaoChamadoService>();

builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();

builder.Services.AddScoped<IMovimentacaoEstoqueRepository, MovimentacaoEstoqueRepository>();
builder.Services.AddScoped<IMovimentacaoEstoqueService, MovimentacaoEstoqueService>();

builder.Services.AddScoped<IRoyaltyRepository, RoyaltyRepository>();
builder.Services.AddScoped<IRoyaltyService, RoyaltyService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped<IVendaService, VendaService>();

builder.Services.AddScoped<IItemVendaRepository, ItemVendaRepository>();
builder.Services.AddScoped<IItemVendaService, ItemVendaService>();

builder.Services.AddScoped<ITaxaFranquiaRepository, TaxaFranquiaRepository>();
builder.Services.AddScoped<ITaxaFranquiaService, TaxaFranquiaService>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!
            )),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new UtcDateTimeJsonConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT no formato: Bearer {token}"
    });

    options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

    var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
    var franquiaRepository = scope.ServiceProvider.GetRequiredService<IFranquiaRepository>();

    var emailAdmin = builder.Configuration["Seed:AdminEmail"] ?? "admin@franquias.com";
    var senhaAdmin = builder.Configuration["Seed:AdminSenha"] ?? "Admin@123";

    var adminExistente = await usuarioRepository.ObterPorEmailAsync(emailAdmin.ToLowerInvariant());

    if (adminExistente is null)
    {
        var franquias = await franquiaRepository.ObterTodasAsync();
        var franquia = franquias.FirstOrDefault()
            ?? await franquiaRepository.AdicionarAsync(
                new Franquia("Rede Principal", "00000000000000", "Av. Principal, 1000"));

        var admin = new Usuario(
            "Administrador",
            emailAdmin.ToLowerInvariant(),
            BCrypt.Net.BCrypt.HashPassword(senhaAdmin),
            PerfilUsuario.Administrador,
            franquia.Id);

        await usuarioRepository.AdicionarAsync(admin);
    }
}

app.Run();

internal class UtcDateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var valor = reader.GetDateTime();

        return valor.Kind == DateTimeKind.Utc
            ? valor
            : DateTime.SpecifyKind(valor, DateTimeKind.Utc);
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(
            value.Kind == DateTimeKind.Utc
                ? value
                : DateTime.SpecifyKind(value, DateTimeKind.Utc));
    }
}

internal class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado na requisição.");

            int statusCode;
            string mensagem;

            switch (ex)
            {
                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    mensagem = ex.Message;
                    break;
                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    mensagem = ex.Message;
                    break;
                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    mensagem = ex.Message;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    mensagem = "Ocorreu um erro interno no servidor.";
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var corpo = JsonSerializer.Serialize(new { mensagem });
            await context.Response.WriteAsync(corpo);
        }
    }
}