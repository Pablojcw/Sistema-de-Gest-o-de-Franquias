# AGENTS.md

## Visão geral

Este repositório contém o **Sistema de Franquias**, uma API REST para administrar franquias, unidades, usuários, produtos, estoque, vendas, fornecedores, royalties, taxas e chamados.

A solução usa .NET 10 e está no arquivo `SistemaDeFranquias.slnx`. O formato da solução é XML; não existe um arquivo `.sln` tradicional.

O código segue uma arquitetura em camadas, próxima de Clean Architecture/DDD simplificado. Cada projeto tem uma responsabilidade definida e as referências entre projetos impedem que uma camada conheça detalhes de uma camada mais externa.

## Estrutura da solução

### `Franquias.Domain`

É o núcleo do sistema. Contém as regras e os modelos de negócio:

- Entidades em `Entities/`, como `Franquia`, `Unidade`, `Usuario`, `Produto`, `Venda` e `Estoque`.
- Enumerações em `Enums/`.
- Objetos de valor em `ValueObjects/`.
- Exceções específicas do domínio em `Exceptions/`.

Este projeto não deve depender de `Application`, `Infrastructure` ou `Api`, nem de Entity Framework. As entidades não usam anotações do EF. Normalmente possuem setters privados, criam o próprio `Id` com `Guid.NewGuid()` no construtor e expõem métodos para manter as regras de negócio protegidas.

### `Franquias.Application`

Contém os casos de uso da aplicação e as abstrações que as camadas externas implementam:

- DTOs de entrada e saída em `DTOs/<Entidade>/`.
- Interfaces de serviços em `Services/Interfaces/`.
- Implementações dos serviços em `Services/`.
- Interfaces de repositórios em `Interfaces/Repositories/`.
- Validadores em `Validators/`.

Este projeto pode referenciar apenas `Franquias.Domain`. Não coloque aqui `AppDbContext`, `DbSet`, `Microsoft.EntityFrameworkCore`, classes de configuração do EF ou classes concretas de repositório.

Os DTOs pertencem a esta camada, não à API. Isso mantém os contratos dos casos de uso separados das entidades e do transporte HTTP.

### `Franquias.Infrastructure`

Implementa o acesso a dados e detalhes técnicos:

- `Data/AppDbContext.cs`: contexto do Entity Framework Core e os `DbSet` das entidades.
- `Data/Configurations/`: configurações Fluent API, uma por entidade quando necessário.
- `Repositories/`: implementações das interfaces de repositório definidas em `Application`.
- `Migrations/`: histórico de alterações do banco gerado pelo EF Core.

Este projeto referencia `Application` e `Domain`. É aqui que podem ser usados Entity Framework Core e o provedor PostgreSQL (`Npgsql.EntityFrameworkCore.PostgreSQL`).

### `Franquias.Api`

É a camada de entrada HTTP:

- `Controllers/`: endpoints REST.
- `Program.cs`: composição da aplicação, injeção de dependências, autenticação, autorização, Swagger, middleware, migrations e seed inicial.
- `appsettings.json` e `appsettings.Development.json`: configurações por ambiente.
- `Properties/launchSettings.json`: perfis locais de execução.

As pastas `DTOs/`, `Configurations/` e `Repositories/` da API são apenas estrutura antiga/vazia. Não adicione implementações nelas. DTOs ficam em `Application` e repositórios ficam em `Infrastructure`.

### `Franquias.Tests`

Projeto de testes com xUnit. Ele deve referenciar `Application` e `Domain`, mantendo os testes de casos de uso e regras de negócio independentes do banco e da API. Organize os testes nas pastas `Application/`, `Domain/` e `Infrastructure/` conforme a camada testada.

## Fluxo completo de uma requisição

O fluxo padrão é:

```text
Cliente HTTP
	-> Controller da API
	-> Interface de serviço (Application/Services/Interfaces)
	-> Implementação do serviço (Application/Services)
	-> Interface de repositório (Application/Interfaces/Repositories)
	-> Implementação do repositório (Infrastructure/Repositories)
	-> AppDbContext (Infrastructure/Data)
	-> PostgreSQL
```

1. O controller recebe a requisição e valida o modelo por meio de `[ApiController]` e dos DTOs.
2. O controller chama o serviço por uma interface, sem conhecer a implementação concreta.
3. O serviço executa o caso de uso, aplica regras e transforma DTOs em entidades ou entidades em respostas.
4. O serviço chama o repositório por uma interface.
5. O repositório usa o `AppDbContext` para consultar ou gravar no PostgreSQL.
6. O resultado volta pelo mesmo caminho até virar uma resposta HTTP.

Não faça o controller acessar o `AppDbContext` diretamente. Não faça o serviço depender da implementação concreta do repositório.

## Entity Framework Core

O `AppDbContext` está em `Franquias.Infrastructure.Data`. Ele recebe `DbContextOptions<AppDbContext>` por injeção de dependência e expõe um `DbSet` para cada agregado persistido.

O método `OnModelCreating` usa `ApplyConfigurationsFromAssembly`, portanto o EF localiza automaticamente as classes `IEntityTypeConfiguration<T>` em `Data/Configurations/`. Relacionamentos, chaves, índices, nomes de tabela, tamanhos e regras de nulidade devem ser configurados pela Fluent API, sem anotações nas entidades.

Regras para repositórios:

- Consultas de leitura devem usar `AsNoTracking()`.
- Use métodos assíncronos do EF, como `FirstOrDefaultAsync`, `ToListAsync`, `AddAsync` e `SaveChangesAsync`.
- Em criações, adicione a entidade nova e salve; não use `Attach` ou `Update` para simular uma criação.
- Não coloque lógica de negócio complexa no repositório.

O `Program.cs` configura o PostgreSQL com `UseNpgsql` e a chave `ConnectionStrings:DefaultConnection`.

## Comportamento atual do `Program.cs`

A inicialização atual faz o seguinte:

1. Registra `AppDbContext` com PostgreSQL.
2. Registra manualmente cada serviço e repositório como `Scoped`.
3. Registra `IAuthService` para autenticação.
4. Configura JWT Bearer com issuer, audience, chave secreta e validade vindos da configuração.
5. Define uma política global que exige autenticação, salvo endpoints explicitamente liberados com `[AllowAnonymous]`.
6. Registra controllers, serialização JSON, Swagger e OpenAPI.
7. Habilita Swagger apenas no ambiente de desenvolvimento.
8. Instala `ExceptionHandlingMiddleware` antes da autenticação e autorização.
9. Executa `Database.MigrateAsync()` ao iniciar a aplicação.
10. Cria uma franquia e um usuário administrador caso o administrador configurado ainda não exista.

O conversor `UtcDateTimeJsonConverter` normaliza valores `DateTime` para UTC na leitura e na escrita JSON.

O `ExceptionHandlingMiddleware` converte exceções conhecidas em respostas HTTP:

- `ArgumentException`: 400.
- `UnauthorizedAccessException`: 401.
- `KeyNotFoundException`: 404.
- Outras exceções: 500, sem expor detalhes internos ao cliente.

## Módulos atuais

Os módulos expostos pela API incluem:

- Autenticação e usuários.
- Franquias, franqueados e unidades.
- Fornecedores e produtos de fornecedores.
- Produtos, estoque e movimentações de estoque.
- Vendas e itens de venda.
- Royalties e taxas de franquia.
- Chamados e atualizações de chamados.
- Relatórios.

Ao criar um novo módulo, siga o conjunto completo: entidade no domínio, DTOs, interface e implementação de serviço, interface e implementação de repositório, configuração Fluent API quando necessária, controller, registro no `Program.cs`, migration e testes.

## Convenções de código

- Use nomes em português e padrão PascalCase: `CriarFranquiaRequest`, `FranquiaResponse`, `CriarAsync`, `ObterPorIdAsync` e `ObterTodasAsync`.
- Use `Async` em métodos assíncronos e aceite `CancellationToken` quando o padrão local permitir.
- Mantenha nullable habilitado e corrija avisos de nulidade em vez de usar `!` sem justificativa.
- Prefira interfaces nas fronteiras entre camadas.
- Registre todo novo serviço e repositório manualmente em `Franquias.Api/Program.cs` com ciclo de vida `Scoped`.
- Controllers devem usar `[ApiController]` e rotas no padrão `[Route("api/[controller]")]`.
- Preserve os nomes públicos existentes para não quebrar controllers, migrations ou consumidores da API.
- Não faça refatorações amplas junto com uma correção específica.

## Configuração e segredos

A conexão local padrão está em `Franquias.Api/appsettings.json`, usando PostgreSQL em `localhost:5432`, banco `FranquiasDb` e usuário `postgres`. A senha e a chave JWT não devem ser gravadas em commits.

O projeto da API possui `UserSecretsId` igual a `FranquiasApi-UserSecrets`. Em desenvolvimento, prefira User Secrets ou variáveis de ambiente para senha do banco, `Jwt:Secret`, `Jwt:Issuer`, `Jwt:Audience`, `Seed:AdminEmail` e `Seed:AdminSenha`.

Não exiba tokens, senhas ou strings de conexão completas em logs, documentação pública ou mensagens de commit.

## Comandos principais

Na raiz da solução:

```powershell
# Restaurar dependências e compilar
dotnet build SistemaDeFranquias.slnx

# Executar a API
dotnet run --project Franquias.Api

# Executar testes
dotnet test

# Criar uma migration no projeto correto
dotnet ef migrations add NomeDaMigration `
	--project Franquias.Infrastructure `
	--startup-project Franquias.Api

# Aplicar migrations manualmente
dotnet ef database update `
	--project Franquias.Infrastructure `
	--startup-project Franquias.Api
```

O perfil HTTP local usa normalmente `http://localhost:5214`. Swagger fica disponível somente em Development, conforme o ambiente configurado.

As migrations devem ser criadas em `Franquias.Infrastructure/Migrations`. O comando do EF precisa de um PostgreSQL acessível, a menos que o contexto seja configurado de outra forma para um cenário de teste.

## Estado conhecido e cuidados

- A solução não possui `global.json`, `Directory.Build.props`, `.editorconfig`, CI, Docker ou script de provisionamento do PostgreSQL.
- `bin/` e `obj/` são artefatos de compilação e não devem ser versionados.
- A inicialização aplica migrations automaticamente; em produção, avalie se esse comportamento atende ao processo de deploy.
- O seed usa valores padrão para o administrador quando as configurações não estão definidas. Esses valores devem ser sobrescritos em ambientes reais.
- Não remova arquivos novos do módulo de Unidade ou de outros módulos apenas porque aparecem como não rastreados no Git; confirme antes se fazem parte de trabalho em andamento.
- Antes de concluir uma alteração, execute pelo menos `dotnet build`; para mudanças de comportamento, execute também o teste específico ou `dotnet test`.
