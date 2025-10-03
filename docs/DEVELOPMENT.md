# Guia de Desenvolvimento - CrudApplication

## 📋 Visão Geral

Este documento fornece informações para desenvolvedores que desejam contribuir, estender ou manter o projeto CrudApplication.

## 🛠️ Ambiente de Desenvolvimento

### Configuração do Ambiente

#### 1. Pré-requisitos
```bash
# Verificar versões
dotnet --version          # >= 8.0.0
mysql --version           # >= 8.0.0
git --version             # >= 2.30.0
```

#### 2. Configuração do Projeto
```bash
# Clone e configuração inicial
git clone <repository-url>
cd CrudApplication
dotnet restore
dotnet build
```

#### 3. Configuração do Banco de Desenvolvimento
```sql
-- Conectar ao PostgreSQL
psql -U postgres

-- Criar banco de desenvolvimento
CREATE DATABASE cadastrodb_dev;

-- Criar usuário de desenvolvimento
CREATE USER dev_user WITH PASSWORD 'dev_password';
GRANT ALL PRIVILEGES ON DATABASE cadastrodb_dev TO dev_user;

-- Sair
\q
```

#### 4. Configuração de Ambiente
```json
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cadastrodb_dev;Username=dev_user;Password=dev_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

## 🔧 Comandos de Desenvolvimento

### Comandos Básicos
```bash
# Executar aplicação
dotnet run

# Executar com hot reload
dotnet watch run

# Executar testes
dotnet test

# Limpar build
dotnet clean

# Restaurar pacotes
dotnet restore
```

### Comandos Entity Framework
```bash
# Adicionar nova migração
dotnet ef migrations add NomeDaMigracao

# Aplicar migrações
dotnet ef database update

# Remover última migração
dotnet ef migrations remove

# Gerar script SQL
dotnet ef migrations script

# Verificar status das migrações
dotnet ef migrations list
```

### Comandos de Build
```bash
# Build de desenvolvimento
dotnet build

# Build de release
dotnet build -c Release

# Publicar aplicação
dotnet publish -c Release -o ./publish
```

## 🏗️ Estrutura do Projeto

### Organização de Arquivos
```
CrudApplication/
├── Controllers/          # Controladores MVC
│   ├── ClientesController.cs
│   ├── ProdutoController.cs
│   └── HomeController.cs
├── Models/              # Modelos de dados
│   ├── Cliente.cs
│   ├── Produto.cs
│   └── ErrorViewModel.cs
├── Data/                # Contexto do banco
│   └── ApplicationDbContext.cs
├── Views/               # Views Razor
│   ├── Clientes/
│   ├── Produto/
│   ├── Home/
│   └── Shared/
├── Migrations/          # Migrações EF
├── wwwroot/            # Arquivos estáticos
├── Properties/         # Configurações
└── Program.cs          # Ponto de entrada
```

### Convenções de Nomenclatura

#### Controllers
- Nome: `{Entity}Controller`
- Exemplo: `ClientesController`, `ProdutoController`

#### Models
- Nome: Singular da entidade
- Exemplo: `Cliente`, `Produto`

#### Views
- Estrutura: `Views/{Controller}/{Action}.cshtml`
- Exemplo: `Views/Clientes/Index.cshtml`

#### Migrations
- Nome: `{Timestamp}_{NomeDaMigracao}`
- Exemplo: `20251002015418_InitialCreateMySql`

## 🔄 Fluxo de Desenvolvimento

### 1. Adicionando Nova Entidade

#### Passo 1: Criar Model
```csharp
// Models/NovaEntidade.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApplication.Models
{
    [Table("NovaEntidade")]
    public class NovaEntidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
```

#### Passo 2: Atualizar DbContext
```csharp
// Data/ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
    // ... código existente ...
    
    public DbSet<NovaEntidade> NovaEntidades { get; set; }
}
```

#### Passo 3: Criar Migração
```bash
dotnet ef migrations add AdicionarNovaEntidade
dotnet ef database update
```

#### Passo 4: Criar Controller
```csharp
// Controllers/NovaEntidadeController.cs
public class NovaEntidadeController : Controller
{
    private readonly ApplicationDbContext _context;

    public NovaEntidadeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Implementar ações CRUD...
}
```

#### Passo 5: Criar Views
```bash
# Usar scaffolding (se disponível)
dotnet aspnet-codegenerator controller -m NovaEntidade -dc ApplicationDbContext -api --relativeFolderPath Controllers
```

### 2. Modificando Entidade Existente

#### Passo 1: Alterar Model
```csharp
// Adicionar nova propriedade
public string NovaPropriedade { get; set; }
```

#### Passo 2: Criar Migração
```bash
dotnet ef migrations add AdicionarNovaPropriedade
```

#### Passo 3: Atualizar Views e Controller
- Atualizar formulários
- Atualizar validações
- Atualizar Bind attributes

#### Passo 4: Aplicar Migração
```bash
dotnet ef database update
```

## 🧪 Testes

### Configuração de Testes
```csharp
// Tests/CrudApplication.Tests.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.4.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.0" />
  </ItemGroup>
</Project>
```

### Testes Unitários
```csharp
// Tests/Controllers/ClientesControllerTests.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using CrudApplication.Data;
using CrudApplication.Models;

namespace Tests.Controllers
{
    public class ClientesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ClientesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Index_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Clientes");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            var client = _factory.CreateClient();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Nome", "Teste"),
                new KeyValuePair<string, string>("Sobrenome", "Silva"),
                new KeyValuePair<string, string>("Email", "teste@email.com"),
                new KeyValuePair<string, string>("DataCadastro", DateTime.Now.ToString()),
                new KeyValuePair<string, string>("Ativo", "true")
            });

            var response = await client.PostAsync("/Clientes/Create", formData);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
```

### Testes de Integração
```csharp
// Tests/Data/ApplicationDbContextTests.cs
public class ApplicationDbContextTests
{
    private DbContextOptions<ApplicationDbContext> _options;

    public ApplicationDbContextTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CanCreateCliente()
    {
        using var context = new ApplicationDbContext(_options);
        var cliente = new Cliente
        {
            Nome = "Teste",
            Sobrenome = "Silva",
            Email = "teste@email.com",
            DataCadastro = DateTime.Now,
            Ativo = true
        };

        context.Clientes.Add(cliente);
        context.SaveChanges();

        Assert.Equal(1, context.Clientes.Count());
    }
}
```

## 🔍 Debugging

### Configuração de Debug
```json
// launch.json (VS Code)
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (web)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/bin/Debug/net8.0/CrudApplication.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
            "serverReadyAction": {
                "action": "openExternally",
                "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
            },
            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "sourceFileMap": {
                "/Views": "${workspaceFolder}/Views"
            }
        }
    ]
}
```

### Logging em Desenvolvimento
```csharp
// Program.cs
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
```

### Configuração de Logging
```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.Hosting.Lifetime": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

## 📊 Performance

### Otimizações de Query
```csharp
// Evitar N+1 queries
var produtos = await _context.Produtos
    .Include(p => p.Cliente)
    .ToListAsync();

// Usar AsNoTracking para consultas somente leitura
var clientes = await _context.Clientes
    .AsNoTracking()
    .ToListAsync();

// Paginação
var clientes = await _context.Clientes
    .Skip(page * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

### Configurações de Performance
```csharp
// Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30));
    });
    
    // Para desenvolvimento
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});
```

## 🔒 Segurança

### Validações
```csharp
// Model validation
[Required(ErrorMessage = "Campo obrigatório")]
[StringLength(100, MinimumLength = 2, ErrorMessage = "Entre 2 e 100 caracteres")]
[EmailAddress(ErrorMessage = "Email inválido")]
public string Email { get; set; }
```

### Proteção CSRF
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Cliente cliente)
{
    // Implementação...
}
```

### Sanitização de Input
```csharp
// Usar HtmlEncoder para sanitizar output
@Html.DisplayFor(model => model.Nome)

// Validar e limpar input
public static string SanitizeInput(string input)
{
    if (string.IsNullOrEmpty(input))
        return string.Empty;
    
    return HtmlEncoder.Default.Encode(input);
}
```

## 📝 Code Style

### Convenções C#
```csharp
// Nomenclatura
public class ClienteController : Controller  // PascalCase para classes
private readonly ApplicationDbContext _context;  // camelCase para campos privados

// Propriedades
public string Nome { get; set; }  // PascalCase para propriedades públicas

// Métodos
public async Task<IActionResult> Index()  // PascalCase para métodos públicos
{
    // Implementação...
}

// Variáveis locais
var clientes = await _context.Clientes.ToListAsync();  // camelCase para variáveis
```

### Comentários e Documentação
```csharp
/// <summary>
/// Controller responsável por gerenciar operações CRUD de clientes.
/// </summary>
public class ClientesController : Controller
{
    /// <summary>
    /// Lista todos os clientes cadastrados no sistema.
    /// </summary>
    /// <returns>View com lista de clientes</returns>
    public async Task<IActionResult> Index()
    {
        // Implementação...
    }
}
```

## 🚀 Deploy

### Configuração de Produção
```json
// appsettings.Production.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=prod-server;Database=cadastrodb;Username=prod_user;Password=secure_password;SSL Mode=Require;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Docker
```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CrudApplication/CrudApplication.csproj", "CrudApplication/"]
RUN dotnet restore "CrudApplication/CrudApplication.csproj"
COPY . .
WORKDIR "/src/CrudApplication"
RUN dotnet build "CrudApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudApplication.dll"]
```

## 📚 Recursos Úteis

### Documentação Oficial
- [ASP.NET Core MVC](https://docs.microsoft.com/aspnet/core/mvc/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [PostgreSQL para .NET](https://www.npgsql.org/doc/)

### Ferramentas
- [Entity Framework Tools](https://docs.microsoft.com/ef/core/cli/dotnet)
- [ASP.NET Core Scaffolding](https://docs.microsoft.com/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator)
- [pgAdmin](https://www.pgadmin.org/download/)

### Extensões VS Code
- C# for Visual Studio Code
- PostgreSQL
- REST Client
- Thunder Client

### Extensões Visual Studio
- Entity Framework Core Power Tools
- PostgreSQL for Visual Studio
- Web Essentials

