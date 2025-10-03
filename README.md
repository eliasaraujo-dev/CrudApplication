# CrudApplication

Uma aplicação web ASP.NET Core MVC para gerenciamento de clientes e produtos, desenvolvida em C# com Entity Framework Core e MySQL.

## 📋 Visão Geral

O CrudApplication é uma aplicação web que permite realizar operações CRUD (Create, Read, Update, Delete) em duas entidades principais:
- **Clientes**: Gerenciamento de informações de clientes
- **Produtos**: Gerenciamento de produtos associados a clientes

## 🚀 Funcionalidades

### Gestão de Clientes
- ✅ Listar todos os clientes
- ✅ Visualizar detalhes de um cliente
- ✅ Criar novo cliente
- ✅ Editar informações do cliente
- ✅ Excluir cliente
- ✅ Validação de campos obrigatórios
- ✅ Validação de formato de email

### Gestão de Produtos
- ✅ Listar todos os produtos com informações do cliente
- ✅ Visualizar detalhes de um produto
- ✅ Criar novo produto associado a um cliente
- ✅ Editar informações do produto
- ✅ Excluir produto
- ✅ Relacionamento com cliente (chave estrangeira)

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core MVC** - Arquitetura web
- **Entity Framework Core** - ORM para acesso a dados
- **MySQL** - Banco de dados
- **Pomelo.EntityFrameworkCore.MySql** - Provedor MySQL para EF Core
- **Bootstrap** - Framework CSS para interface
- **jQuery** - JavaScript para interatividade

## 📁 Estrutura do Projeto

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
├── Data/                # Contexto do banco de dados
│   └── ApplicationDbContext.cs
├── Views/               # Views Razor
│   ├── Clientes/
│   ├── Produto/
│   ├── Home/
│   └── Shared/
├── Migrations/          # Migrações do banco de dados
├── wwwroot/            # Arquivos estáticos
└── Program.cs          # Ponto de entrada da aplicação
```

## 🗄️ Modelo de Dados

### Entidade Cliente
- `IdCliente` (int, PK) - Identificador único
- `Nome` (string, obrigatório) - Nome do cliente
- `Sobrenome` (string, obrigatório) - Sobrenome do cliente
- `Email` (string, obrigatório) - Email com validação
- `DataCadastro` (DateTime) - Data de cadastro
- `Ativo` (bool) - Status ativo/inativo
- `Produtos` (ICollection) - Relacionamento com produtos

### Entidade Produto
- `IdProduto` (int, PK) - Identificador único
- `Nome` (string, obrigatório) - Nome do produto
- `Valor` (decimal) - Preço do produto
- `Disponivel` (bool) - Disponibilidade do produto
- `IdCliente` (int, FK) - Referência ao cliente
- `Cliente` (Cliente) - Propriedade de navegação

## 🔧 Instalação e Configuração

### Pré-requisitos
- .NET 8.0 SDK
- MySQL Server
- Visual Studio 2022 ou VS Code

### Passos para instalação

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd CrudApplication
   ```

2. **Configure o banco de dados**
   - Instale o MySQL Server
   - Crie um banco de dados chamado `cadastrodb`
   - Atualize a connection string no `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=cadastrodb;Uid=seu_usuario;Pwd=sua_senha;"
     }
   }
   ```

3. **Execute as migrações**
   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse a aplicação**
   - Abra o navegador em `http://localhost:5031`

## 📚 Documentação Técnica

### Controllers

#### ClientesController
- **GET** `/Clientes` - Lista todos os clientes
- **GET** `/Clientes/Details/{id}` - Visualiza detalhes de um cliente
- **GET** `/Clientes/Create` - Formulário de criação
- **POST** `/Clientes/Create` - Cria novo cliente
- **GET** `/Clientes/Edit/{id}` - Formulário de edição
- **POST** `/Clientes/Edit/{id}` - Atualiza cliente
- **GET** `/Clientes/Delete/{id}` - Confirmação de exclusão
- **POST** `/Clientes/Delete/{id}` - Exclui cliente

#### ProdutoController
- **GET** `/Produto` - Lista todos os produtos com clientes
- **GET** `/Produto/Details/{id}` - Visualiza detalhes de um produto
- **GET** `/Produto/Create` - Formulário de criação
- **POST** `/Produto/Create` - Cria novo produto
- **GET** `/Produto/Edit/{id}` - Formulário de edição
- **POST** `/Produto/Edit/{id}` - Atualiza produto
- **GET** `/Produto/Delete/{id}` - Confirmação de exclusão
- **POST** `/Produto/Delete/{id}` - Exclui produto

### Validações

#### Cliente
- Nome e Sobrenome são obrigatórios (máximo 50 e 100 caracteres)
- Email é obrigatório e deve ter formato válido (máximo 150 caracteres)

#### Produto
- Nome é obrigatório (máximo 100 caracteres)
- Valor é obrigatório (tipo decimal)
- Deve estar associado a um cliente existente

### Relacionamentos
- **Cliente → Produto**: Um cliente pode ter vários produtos (1:N)
- **Produto → Cliente**: Cada produto pertence a um cliente (N:1)

## 🚦 Como Usar

1. **Gerenciar Clientes**:
   - Acesse `/Clientes` para ver a lista
   - Clique em "Create New" para adicionar um cliente
   - Use os links "Edit", "Details" ou "Delete" para outras operações

2. **Gerenciar Produtos**:
   - Acesse `/Produto` para ver a lista
   - Clique em "Create New" para adicionar um produto
   - Selecione um cliente na lista suspensa
   - Use os links "Edit", "Details" ou "Delete" para outras operações

## 🔒 Segurança

- Validação de tokens anti-falsificação (`ValidateAntiForgeryToken`)
- Validação de entrada de dados com Data Annotations
- Proteção contra ataques de overposting
- Validação de estado do modelo antes de operações de banco

## 📝 Licença

Este projeto é para fins educacionais e de demonstração.

## 👨‍💻 Desenvolvedor

Desenvolvido como exemplo de aplicação CRUD com ASP.NET Core MVC e Entity Framework Core.

---

**Versão**: 1.0.0  
**Última atualização**: Janeiro 2025
