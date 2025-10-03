# Guia de Instalação e Configuração - CrudApplication

## 📋 Pré-requisitos

### Software Necessário

| Software | Versão Mínima | Descrição |
|----------|---------------|-----------|
| .NET 8.0 SDK | 8.0.0 | Framework de desenvolvimento |
| MySQL Server | 8.0+ | Banco de dados |
| Visual Studio 2022 | 17.8+ | IDE (opcional) |
| VS Code | 1.80+ | Editor alternativo |

### Ferramentas Recomendadas

- **MySQL Workbench**: Interface gráfica para MySQL
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

## 🗄️ Instalação do MySQL

### Windows

1. **Download**:
   - Acesse: https://dev.mysql.com/downloads/mysql/
   - Baixe MySQL Community Server 8.0+

2. **Instalação**:
   - Execute o instalador
   - Escolha "Developer Default"
   - Configure senha do root
   - Complete a instalação

3. **Verificação**:
   ```cmd
   mysql --version
   ```

### macOS

```bash
# Via Homebrew
brew install mysql

# Iniciar serviço
brew services start mysql

# Configurar segurança
mysql_secure_installation
```

### Linux (Ubuntu/Debian)

```bash
# Atualizar pacotes
sudo apt update

# Instalar MySQL Server
sudo apt install mysql-server

# Configurar segurança
sudo mysql_secure_installation

# Verificar status
sudo systemctl status mysql
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
-- Conectar ao MySQL como root
mysql -u root -p

-- Criar banco de dados
CREATE DATABASE cadastrodb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Criar usuário específico (opcional)
CREATE USER 'cruduser'@'localhost' IDENTIFIED BY 'senha_segura';
GRANT ALL PRIVILEGES ON cadastrodb.* TO 'cruduser'@'localhost';
FLUSH PRIVILEGES;

-- Sair do MySQL
EXIT;
```

#### Configurar Connection String

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cadastrodb;Uid=root;Pwd=sua_senha_aqui;"
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
    "DefaultConnection": "Server=localhost;Port=3306;Database=cadastrodb;Uid=root;Pwd=sua_senha_aqui;"
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
mysql -u root -p cadastrodb

-- Verificar tabelas criadas
SHOW TABLES;

-- Verificar estrutura da tabela Clientes
DESCRIBE Clientes;

-- Verificar estrutura da tabela Produtos
DESCRIBE Produtos;

-- Sair
EXIT;
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

#### 1. Erro de Conexão com MySQL

**Erro**: `Unable to connect to any of the specified MySQL hosts`

**Soluções**:
- Verificar se MySQL está rodando: `sudo systemctl status mysql`
- Verificar porta: padrão é 3306
- Verificar credenciais na connection string
- Verificar firewall

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
- Verificar privilégios do usuário no MySQL
- Usar usuário root temporariamente

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

# Verificar status do MySQL
sudo systemctl status mysql

# Testar conexão MySQL
mysql -u root -p -e "SELECT VERSION();"

# Verificar logs da aplicação
dotnet run --verbosity detailed
```

## 📊 Verificação da Instalação

### Checklist de Verificação

- [ ] .NET 8.0 SDK instalado e funcionando
- [ ] MySQL Server instalado e rodando
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
    "DefaultConnection": "Server=localhost;Port=3306;Database=cadastrodb;Uid=usuario_dedicado;Pwd=senha_forte;SslMode=Required;"
  }
}
```

### Variáveis de Ambiente

Para produção, use variáveis de ambiente:

```bash
# Definir variáveis
export ConnectionStrings__DefaultConnection="Server=prod-server;Database=cadastrodb;Uid=prod_user;Pwd=senha_super_segura;"

# Executar aplicação
dotnet run
```

### Firewall

```bash
# Liberar porta MySQL (se necessário)
sudo ufw allow 3306

# Liberar porta da aplicação
sudo ufw allow 5031
```

## 📈 Otimizações para Produção

### Configurações de Performance

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cadastrodb;Uid=user;Pwd=pass;Pooling=true;MinimumPoolSize=5;MaximumPoolSize=100;"
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

## 📝 Próximos Passos

Após a instalação bem-sucedida:

1. **Configurar ambiente de desenvolvimento**
2. **Implementar testes unitários**
3. **Configurar CI/CD**
4. **Implementar logging estruturado**
5. **Configurar monitoramento**
6. **Implementar backup do banco**
