# Documentação do CrudApplication

Bem-vindo à documentação completa do CrudApplication! Esta seção contém toda a documentação técnica e de usuário do projeto.

## 📚 Índice da Documentação

### 📖 Documentação Principal
- **[README.md](../README.md)** - Visão geral do projeto, funcionalidades e guia rápido

### 🏗️ Documentação Técnica
- **[ARCHITECTURE.md](ARCHITECTURE.md)** - Arquitetura do sistema, padrões e fluxos de dados
- **[MODELS.md](MODELS.md)** - Documentação detalhada dos modelos de dados
- **[CONTROLLERS.md](CONTROLLERS.md)** - Documentação dos controllers e funcionalidades CRUD
- **[API.md](API.md)** - Documentação dos endpoints e rotas da aplicação

### 🔧 Guias de Configuração
- **[INSTALLATION.md](INSTALLATION.md)** - Guia completo de instalação e configuração
- **[DEVELOPMENT.md](DEVELOPMENT.md)** - Guia para desenvolvedores e contribuidores

## 🚀 Início Rápido

### Para Usuários
1. Leia o **[README.md](../README.md)** para entender o projeto
2. Siga o **[INSTALLATION.md](INSTALLATION.md)** para configurar o ambiente
3. Execute `dotnet run` e acesse `http://localhost:5031`

### Para Desenvolvedores
1. Consulte **[DEVELOPMENT.md](DEVELOPMENT.md)** para configurar o ambiente de desenvolvimento
2. Leia **[ARCHITECTURE.md](ARCHITECTURE.md)** para entender a estrutura do projeto
3. Estude **[MODELS.md](MODELS.md)** e **[CONTROLLERS.md](CONTROLLERS.md)** para entender a implementação

### Para Integração
1. Consulte **[API.md](API.md)** para entender os endpoints disponíveis
2. Use os exemplos de requisições para integração

## 📊 Resumo do Projeto

### Tecnologias
- **.NET 8.0** - Framework principal
- **ASP.NET Core MVC** - Arquitetura web
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de dados
- **Bootstrap** - Interface

### Funcionalidades
- ✅ CRUD completo para Clientes
- ✅ CRUD completo para Produtos
- ✅ Relacionamento Cliente-Produto (1:N)
- ✅ Validações robustas
- ✅ Interface web responsiva
- ✅ Segurança CSRF

### Estrutura
```
CrudApplication/
├── Controllers/     # Controladores MVC
├── Models/         # Modelos de dados
├── Views/          # Views Razor
├── Data/           # Contexto EF Core
├── Migrations/     # Migrações do banco
└── wwwroot/        # Arquivos estáticos
```

## 🔍 Navegação da Documentação

### Por Tipo de Usuário

#### 👤 Usuário Final
- [README.md](../README.md) - Como usar a aplicação
- [INSTALLATION.md](INSTALLATION.md) - Como instalar

#### 👨‍💻 Desenvolvedor
- [DEVELOPMENT.md](DEVELOPMENT.md) - Ambiente de desenvolvimento
- [ARCHITECTURE.md](ARCHITECTURE.md) - Estrutura do projeto
- [MODELS.md](MODELS.md) - Modelos de dados
- [CONTROLLERS.md](CONTROLLERS.md) - Lógica de negócio

#### 🔌 Integrador
- [API.md](API.md) - Endpoints e integração
- [INSTALLATION.md](INSTALLATION.md) - Configuração do servidor

### Por Tópico

#### 🏗️ Arquitetura
- [ARCHITECTURE.md](ARCHITECTURE.md) - Visão geral da arquitetura
- [MODELS.md](MODELS.md) - Camada de dados
- [CONTROLLERS.md](CONTROLLERS.md) - Camada de aplicação

#### 🔧 Configuração
- [INSTALLATION.md](INSTALLATION.md) - Instalação e configuração
- [DEVELOPMENT.md](DEVELOPMENT.md) - Ambiente de desenvolvimento

#### 📡 Integração
- [API.md](API.md) - Documentação da API
- [README.md](../README.md) - Exemplos de uso

## 📝 Convenções da Documentação

### Formatação
- **Código**: Usar blocos de código com syntax highlighting
- **Comandos**: Usar blocos de terminal
- **Tabelas**: Para informações estruturadas
- **Emojis**: Para categorização visual

### Estrutura
- Cada documento tem um índice no início
- Seções bem organizadas com hierarquia clara
- Exemplos práticos sempre que possível
- Links para documentos relacionados

### Atualizações
- Documentação deve ser mantida atualizada com o código
- Versionar mudanças significativas
- Manter consistência entre documentos

## 🤝 Contribuindo com a Documentação

### Como Contribuir
1. Identifique áreas que precisam de melhoria
2. Adicione exemplos práticos
3. Corrija informações desatualizadas
4. Melhore a clareza e organização

### Padrões
- Manter consistência de formatação
- Usar linguagem clara e objetiva
- Incluir exemplos sempre que possível
- Atualizar índices quando necessário

## 📞 Suporte

### Problemas Comuns
- Consulte **[INSTALLATION.md](INSTALLATION.md)** para problemas de instalação
- Consulte **[DEVELOPMENT.md](DEVELOPMENT.md)** para problemas de desenvolvimento

### Recursos Adicionais
- Documentação oficial do .NET: https://docs.microsoft.com/dotnet/
- Documentação do Entity Framework: https://docs.microsoft.com/ef/
- Documentação do ASP.NET Core: https://docs.microsoft.com/aspnet/core/

---

**Última atualização**: Janeiro 2025  
**Versão da documentação**: 1.0.0
