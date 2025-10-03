# Guia de Instalação e Configuração - CrudApplication

## 📋 Pré-requisitos

### Software Necessário

| Software | Versão Mínima | Descrição |
|----------|---------------|-----------|
| .NET 8.0 SDK | 8.0.0 | Framework de desenvolvimento |
| PostgreSQL Server | 13.0+ | Banco de dados |
| Visual Studio 2022 | 17.8+ | IDE (opcional) |
| VS Code | 1.80+ | Editor alternativo |

### Ferramentas Recomendadas

- **pgAdmin**: Interface gráfica para PostgreSQL
- **Git**: Controle de versão
- **Postman**: Teste de APIs (para futuras implementações)

## 🔧 Instalação do .NET 8.0 SDK

### Windows

1. **Download**:
   - Acesse: https://dotnet.microsoft.com/download/dotnet/8.0
   - Baixe o SDK para Windows x64

2. **Instalação**:
   - Execute o instalador baixado
   - Siga as instruções do instalador
   - Reinicie o terminal/PowerShell

3. **Verificação**:
   ```powershell
   dotnet --version
   ```
   Deve retornar: `8.0.x`

### macOS

```bash
# Via Homebrew
brew install dotnet@8

# Verificação
dotnet --version
```

### Linux (Ubuntu/Debian)

```bash
# Adicionar repositório Microsoft
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Instalar .NET SDK
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Verificação
dotnet --version
```

## 🗄️ Instalação do PostgreSQL

### Windows

1. **Download**:
   - Acesse: https://www.postgresql.org/download/windows/
   - Baixe PostgreSQL 13+ para Windows

2. **Instalação**:
   - Execute o instalador
   - Configure senha do usuário postgres
   - Complete a instalação

3. **Verificação**:
   ```cmd
   psql --version
   ```

### macOS

```bash
# Via Homebrew
brew install postgresql

# Iniciar serviço
brew services start postgresql

# Criar usuário
createuser -s postgres
```

### Linux (Ubuntu/Debian)

```bash
# Atualizar pacotes
sudo apt update

# Instalar PostgreSQL
sudo apt install postgresql postgresql-contrib

# Iniciar serviço
sudo systemctl start postgresql
sudo systemctl enable postgresql

# Verificar status
sudo systemctl status postgresql
```

## 📥 Instalação do Projeto

### 1. Clone do Repositório

```bash
# Clone o repositório
git clone <url-do-repositorio>
cd CrudApplication

# Ou baixe e extraia o arquivo ZIP
```

### 2. Configuração do Banco de Dados

#### Criar Banco de Dados

```sql
-- Conectar ao PostgreSQL como postgres
psql -U postgres

-- Criar banco de dados
CREATE DATABASE cadastrodb;

-- Criar usuário específico (opcional)
CREATE USER cruduser WITH PASSWORD 'senha_segura';
GRANT ALL PRIVILEGES ON DATABASE cadastrodb TO cruduser;

-- Sair do psql
\q
```

#### Configurar Connection String

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cadastrodb;Username=postgres;Password=sua_senha_aqui;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Para ambiente de desenvolvimento**, edite também `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cadastrodb;Username=postgres;Password=sua_senha_aqui;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 3. Restaurar Pacotes NuGet

```bash
# Restaurar dependências
dotnet restore

# Ou se preferir usar o comando completo
dotnet restore CrudApplication/CrudApplication.csproj
```

### 4. Executar Migrações

```bash
# Navegar para o diretório do projeto
cd CrudApplication

# Verificar migrações pendentes
dotnet ef migrations list

# Aplicar migrações ao banco
dotnet ef database update

# Verificar se as tabelas foram criadas
```

### 5. Verificar Estrutura do Banco

```sql
-- Conectar ao banco
psql -U postgres -d cadastrodb

-- Verificar tabelas criadas
\dt

-- Verificar estrutura da tabela Clientes
\d "Clientes"

-- Verificar estrutura da tabela Produtos
\d "Produtos"

-- Sair
\q
```

## 🚀 Executando a Aplicação

### Desenvolvimento

```bash
# Navegar para o diretório do projeto
cd CrudApplication

# Executar em modo desenvolvimento
dotnet run

# Ou especificar o ambiente
dotnet run --environment Development
```

### Produção

```bash
# Publicar a aplicação
dotnet publish -c Release -o ./publish

# Executar a versão publicada
cd publish
dotnet CrudApplication.dll
```

### Acessar a Aplicação

- **URL Local**: http://localhost:5031
- **IIS Express**: http://localhost:62771 (se usando Visual Studio)

## 🔧 Configurações Adicionais

### Portas e URLs

Edite `Properties/launchSettings.json` para alterar portas:

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5031",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### Logging

Configure níveis de log em `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

## 🐛 Solução de Problemas

### Problemas Comuns

#### 1. Erro de Conexão com PostgreSQL

**Erro**: `Unable to connect to any of the specified PostgreSQL hosts`

**Soluções**:
- Verificar se PostgreSQL está rodando: `sudo systemctl status postgresql`
- Verificar porta: padrão é 5432
- Verificar credenciais na connection string
- Verificar firewall
- Verificar arquivo `pg_hba.conf` para autenticação

#### 2. Erro de Migração

**Erro**: `No migrations found`

**Soluções**:
```bash
# Verificar se migrações existem
ls CrudApplication/Migrations/

# Recriar migrações se necessário
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### 3. Erro de Permissão

**Erro**: `Access denied for user`

**Soluções**:
- Verificar usuário e senha
- Verificar privilégios do usuário no PostgreSQL
- Usar usuário postgres temporariamente
- Configurar `pg_hba.conf` para trust local

#### 4. Porta em Uso

**Erro**: `Address already in use`

**Soluções**:
- Alterar porta no `launchSettings.json`
- Encontrar processo usando a porta:
  ```bash
  # Windows
  netstat -ano | findstr :5031
  
  # Linux/macOS
  lsof -i :5031
  ```
- Encerrar processo ou usar porta diferente

### Comandos de Diagnóstico

```bash
# Verificar versão do .NET
dotnet --version

# Verificar se EF Tools está instalado
dotnet ef --version

# Verificar status do PostgreSQL
sudo systemctl status postgresql

# Testar conexão PostgreSQL
psql -U postgres -c "SELECT version();"

# Verificar logs da aplicação
dotnet run --verbosity detailed
```

## 📊 Verificação da Instalação

### Checklist de Verificação

- [ ] .NET 8.0 SDK instalado e funcionando
- [ ] PostgreSQL Server instalado e rodando
- [ ] Banco de dados `cadastrodb` criado
- [ ] Connection string configurada
- [ ] Pacotes NuGet restaurados
- [ ] Migrações aplicadas
- [ ] Aplicação executando sem erros
- [ ] Acesso via navegador funcionando
- [ ] Operações CRUD funcionando

### Teste Básico

1. **Acesse**: http://localhost:5031
2. **Navegue para**: Clientes
3. **Teste**: Criar um cliente
4. **Navegue para**: Produtos
5. **Teste**: Criar um produto associado ao cliente

## 🔒 Configurações de Segurança

### Connection String Segura

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cadastrodb;Username=usuario_dedicado;Password=senha_forte;SSL Mode=Require;"
  }
}
```

### Variáveis de Ambiente

Para produção, use variáveis de ambiente:

```bash
# Definir variáveis
export ConnectionStrings__DefaultConnection="Host=prod-server;Database=cadastrodb;Username=prod_user;Password=senha_super_segura;SSL Mode=Require;"

# Executar aplicação
dotnet run
```

### Firewall

```bash
# Liberar porta PostgreSQL (se necessário)
sudo ufw allow 5432

# Liberar porta da aplicação
sudo ufw allow 5031
```

## 📈 Otimizações para Produção

### Configurações de Performance

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cadastrodb;Username=user;Password=pass;Pooling=true;MinPoolSize=5;MaxPoolSize=100;"
  }
}
```

### Configurações de Log

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Error"
    }
  }
}
```

## 🐳 Docker (Opcional)

### Dockerfile já incluído

O projeto já inclui um `Dockerfile` configurado para deploy em containers.

### Docker Compose para Desenvolvimento

```yaml
# docker-compose.yml
version: '3.8'
services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_DB: cadastrodb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: senha123
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  app:
    build: .
    ports:
      - "5031:80"
    depends_on:
      - postgres
    environment:
      ConnectionStrings__DefaultConnection: "Host=postgres;Port=5432;Database=cadastrodb;Username=postgres;Password=senha123;"

volumes:
  postgres_data:
```

## 📝 Próximos Passos

Após a instalação bem-sucedida:

1. **Configurar ambiente de desenvolvimento**
2. **Implementar testes unitários**
3. **Configurar CI/CD**
4. **Implementar logging estruturado**
5. **Configurar monitoramento**
6. **Implementar backup do banco**
