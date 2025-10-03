# Documentação dos Modelos de Dados - CrudApplication

## 📋 Visão Geral

Este documento detalha os modelos de dados utilizados no CrudApplication, suas propriedades, validações, relacionamentos e anotações de mapeamento para o Entity Framework Core.

## 🏗️ Estrutura dos Modelos

### 1. Modelo Cliente

O modelo `Cliente` representa as informações de um cliente no sistema.

#### Propriedades e Anotações

```csharp
[Table("Clientes")] // Define o nome da tabela no banco de dados
public class Cliente
{
    [Key] // Marca como chave primária
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")] // Validação
    [StringLength(50)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
    [StringLength(100)]
    public string? Sobrenome { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    [StringLength(150)]
    public string? Email { get; set; }

    public DateTime DataCadastro { get; set; }

    public bool Ativo { get; set; }

    // Propriedade de navegação: um cliente pode ter vários produtos
    public virtual ICollection<Produto>? Produtos { get; set; }
}
```

#### Detalhamento das Propriedades

| Propriedade | Tipo | Obrigatório | Tamanho | Descrição |
|-------------|------|-------------|---------|-----------|
| `IdCliente` | `int` | ✅ | - | Chave primária, auto-incremento |
| `Nome` | `string` | ✅ | 50 | Nome do cliente |
| `Sobrenome` | `string` | ✅ | 100 | Sobrenome do cliente |
| `Email` | `string` | ✅ | 150 | Email com validação de formato |
| `DataCadastro` | `DateTime` | ✅ | - | Data de cadastro do cliente |
| `Ativo` | `bool` | ✅ | - | Status ativo/inativo (padrão: true) |
| `Produtos` | `ICollection<Produto>` | ❌ | - | Relacionamento com produtos |

#### Validações Implementadas

1. **Nome**:
   - Campo obrigatório
   - Máximo 50 caracteres
   - Mensagem de erro personalizada

2. **Sobrenome**:
   - Campo obrigatório
   - Máximo 100 caracteres
   - Mensagem de erro personalizada

3. **Email**:
   - Campo obrigatório
   - Máximo 150 caracteres
   - Validação de formato de email
   - Mensagem de erro personalizada para formato inválido

#### Mapeamento para Banco de Dados

```sql
CREATE TABLE "Clientes" (
    "IdCliente" SERIAL PRIMARY KEY,
    "Nome" VARCHAR(50) NOT NULL,
    "Sobrenome" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(150) NOT NULL,
    "DataCadastro" TIMESTAMP NOT NULL,
    "Ativo" BOOLEAN NOT NULL DEFAULT TRUE
);
```

### 2. Modelo Produto

O modelo `Produto` representa as informações de um produto no sistema.

#### Propriedades e Anotações

```csharp
[Table("Produtos")]
public class Produto
{
    [Key]
    public int IdProduto { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo Valor é obrigatório.")]
    [Column(TypeName = "decimal(18,2)")] // Define o tipo da coluna no banco
    public decimal Valor { get; set; }

    public bool Disponivel { get; set; }

    // Chave Estrangeira
    [Required(ErrorMessage = "É obrigatório associar um cliente ao produto.")]
    public int IdCliente { get; set; }

    // Propriedade de navegação: este produto pertence a um cliente
    [ForeignKey("IdCliente")]
    public virtual Cliente? Cliente { get; set; }
}
```

#### Detalhamento das Propriedades

| Propriedade | Tipo | Obrigatório | Tamanho | Descrição |
|-------------|------|-------------|---------|-----------|
| `IdProduto` | `int` | ✅ | - | Chave primária, auto-incremento |
| `Nome` | `string` | ✅ | 100 | Nome do produto |
| `Valor` | `decimal` | ✅ | - | Preço do produto (18,2) |
| `Disponivel` | `bool` | ✅ | - | Status de disponibilidade |
| `IdCliente` | `int` | ✅ | - | Chave estrangeira para Cliente |
| `Cliente` | `Cliente` | ❌ | - | Propriedade de navegação |

#### Validações Implementadas

1. **Nome**:
   - Campo obrigatório
   - Máximo 100 caracteres
   - Mensagem de erro personalizada

2. **Valor**:
   - Campo obrigatório
   - Tipo decimal com precisão (18,2)
   - Mensagem de erro personalizada

3. **IdCliente**:
   - Campo obrigatório
   - Deve referenciar um cliente existente
   - Mensagem de erro personalizada

#### Mapeamento para Banco de Dados

```sql
CREATE TABLE "Produtos" (
    "IdProduto" SERIAL PRIMARY KEY,
    "Nome" VARCHAR(100) NOT NULL,
    "Valor" DECIMAL(18,2) NOT NULL,
    "Disponivel" BOOLEAN NOT NULL DEFAULT TRUE,
    "IdCliente" INTEGER NOT NULL,
    FOREIGN KEY ("IdCliente") REFERENCES "Clientes"("IdCliente")
);
```

## 🔗 Relacionamentos

### Relacionamento Cliente-Produto

#### Tipo de Relacionamento
- **Cliente → Produto**: One-to-Many (1:N)
- **Produto → Cliente**: Many-to-One (N:1)

#### Implementação

```csharp
// No modelo Cliente
public virtual ICollection<Produto>? Produtos { get; set; }

// No modelo Produto
[ForeignKey("IdCliente")]
public virtual Cliente? Cliente { get; set; }
public int IdCliente { get; set; }
```

#### Características do Relacionamento

1. **Navegação**:
   - Um cliente pode acessar todos os seus produtos via `cliente.Produtos`
   - Um produto pode acessar seu cliente via `produto.Cliente`

2. **Integridade Referencial**:
   - Foreign Key `IdCliente` na tabela `Produtos`
   - Referência para `IdCliente` na tabela `Clientes`
   - Restrição de chave estrangeira no banco

3. **Comportamento**:
   - Lazy Loading habilitado (virtual properties)
   - Carregamento explícito com `Include()`

## 📊 Contexto do Banco de Dados

### ApplicationDbContext

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Define os DbSets para as entidades
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
}
```

#### Configurações do DbContext

1. **Herança**: Herda de `DbContext` do Entity Framework Core
2. **Injeção de Dependência**: Configurado no `Program.cs`
3. **Connection String**: Configurada via `appsettings.json`
4. **Provedor**: PostgreSQL usando Npgsql.EntityFrameworkCore.PostgreSQL

## 🔍 Validações e Regras de Negócio

### Validações de Entrada

#### Cliente
- Nome e Sobrenome são obrigatórios
- Email deve ter formato válido
- Tamanhos máximos respeitados

#### Produto
- Nome é obrigatório
- Valor deve ser maior que zero (implícito)
- Deve estar associado a um cliente válido

### Regras de Negócio Implementadas

1. **Data de Cadastro**:
   - Definida automaticamente no momento da criação
   - Não editável pelo usuário

2. **Status Ativo**:
   - Cliente: Padrão `true`
   - Produto: Campo `Disponivel` com padrão `true`

3. **Relacionamento**:
   - Produto deve ter um cliente associado
   - Cliente pode existir sem produtos

## 🚀 Otimizações e Performance

### Lazy Loading
```csharp
public virtual ICollection<Produto>? Produtos { get; set; }
public virtual Cliente? Cliente { get; set; }
```

### Carregamento Explícito
```csharp
// Carrega produtos com informações do cliente
var produtos = await _context.Produtos
    .Include(p => p.Cliente)
    .ToListAsync();
```

### Consultas Otimizadas
```csharp
// Carrega apenas clientes ativos
var clientesAtivos = await _context.Clientes
    .Where(c => c.Ativo)
    .ToListAsync();

// Carrega produtos disponíveis com cliente
var produtosDisponiveis = await _context.Produtos
    .Include(p => p.Cliente)
    .Where(p => p.Disponivel)
    .ToListAsync();
```

## 📝 Exemplos de Uso

### Criando um Cliente
```csharp
var cliente = new Cliente
{
    Nome = "João",
    Sobrenome = "Silva",
    Email = "joao.silva@email.com",
    DataCadastro = DateTime.Now,
    Ativo = true
};

_context.Clientes.Add(cliente);
await _context.SaveChangesAsync();
```

### Criando um Produto
```csharp
var produto = new Produto
{
    Nome = "Notebook Dell",
    Valor = 2500.00m,
    Disponivel = true,
    IdCliente = cliente.IdCliente
};

_context.Produtos.Add(produto);
await _context.SaveChangesAsync();
```

### Consultando com Relacionamentos
```csharp
// Busca cliente com todos os produtos
var clienteComProdutos = await _context.Clientes
    .Include(c => c.Produtos)
    .FirstOrDefaultAsync(c => c.IdCliente == id);

// Busca produto com informações do cliente
var produtoComCliente = await _context.Produtos
    .Include(p => p.Cliente)
    .FirstOrDefaultAsync(p => p.IdProduto == id);
```

## 🔧 Migrações

### Migração Inicial
```csharp
// 20251002015418_InitialCreateMySql.cs
public partial class InitialCreateMySql : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clientes",
            columns: table => new
            {
                IdCliente = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                Sobrenome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clientes", x => x.IdCliente);
            });

        migrationBuilder.CreateTable(
            name: "Produtos",
            columns: table => new
            {
                IdProduto = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Disponivel = table.Column<bool>(type: "tinyint(1)", nullable: false),
                IdCliente = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                table.ForeignKey(
                    name: "FK_Produtos_Clientes_IdCliente",
                    column: x => x.IdCliente,
                    principalTable: "Clientes",
                    principalColumn: "IdCliente",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Produtos_IdCliente",
            table: "Produtos",
            column: "IdCliente");
    }
}
```

## 🛡️ Segurança e Integridade

### Proteções Implementadas

1. **Validação de Entrada**:
   - Data Annotations para validação declarativa
   - Validação no servidor e cliente

2. **Integridade Referencial**:
   - Foreign Key constraints
   - Cascade delete configurado

3. **Proteção de Dados**:
   - Campos obrigatórios definidos
   - Tipos de dados apropriados
   - Tamanhos máximos definidos

### Considerações de Segurança

1. **SQL Injection**: Prevenido pelo Entity Framework Core
2. **Overposting**: Protegido por Bind attributes
3. **Validação**: Dupla validação (cliente e servidor)
4. **Integridade**: Constraints de banco de dados

