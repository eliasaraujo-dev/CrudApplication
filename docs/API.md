# Documentação da API - CrudApplication

## 📋 Visão Geral

Este documento descreve as rotas e endpoints disponíveis na aplicação CrudApplication, incluindo parâmetros, respostas e exemplos de uso.

## 🌐 Base URL

- **Desenvolvimento**: `http://localhost:5031`
- **Produção**: `{seu-dominio}`

## 📊 Endpoints Disponíveis

### 1. Clientes

#### 1.1 Listar Clientes
```http
GET /Clientes
```

**Descrição**: Retorna uma lista de todos os clientes cadastrados.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página com tabela de clientes

**Exemplo de Uso**:
```http
GET http://localhost:5031/Clientes
```

---

#### 1.2 Detalhes do Cliente
```http
GET /Clientes/Details/{id}
```

**Descrição**: Exibe detalhes de um cliente específico.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do cliente |

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página com detalhes do cliente

**Exemplo de Uso**:
```http
GET http://localhost:5031/Clientes/Details/1
```

**Códigos de Resposta**:
- `200 OK`: Cliente encontrado
- `404 Not Found`: Cliente não encontrado

---

#### 1.3 Formulário de Criação
```http
GET /Clientes/Create
```

**Descrição**: Exibe formulário para criar novo cliente.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Formulário de criação de cliente

**Exemplo de Uso**:
```http
GET http://localhost:5031/Clientes/Create
```

---

#### 1.4 Criar Cliente
```http
POST /Clientes/Create
```

**Descrição**: Cria um novo cliente.

**Headers**:
```
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
| Campo | Tipo | Obrigatório | Descrição |
|-------|------|-------------|-----------|
| `Nome` | string | ✅ | Nome do cliente (máx. 50 chars) |
| `Sobrenome` | string | ✅ | Sobrenome do cliente (máx. 100 chars) |
| `Email` | string | ✅ | Email do cliente (máx. 150 chars) |
| `DataCadastro` | datetime | ✅ | Data de cadastro |
| `Ativo` | bool | ✅ | Status ativo/inativo |
| `__RequestVerificationToken` | string | ✅ | Token anti-CSRF |

**Exemplo de Body**:
```
Nome=João&Sobrenome=Silva&Email=joao.silva@email.com&DataCadastro=2025-01-02T10:00:00&Ativo=true&__RequestVerificationToken=...
```

**Resposta**:
- **Sucesso**: `302 Redirect` para `/Clientes`
- **Erro de Validação**: `200 OK` com formulário e erros

**Exemplo de Uso**:
```http
POST http://localhost:5031/Clientes/Create
Content-Type: application/x-www-form-urlencoded

Nome=João&Sobrenome=Silva&Email=joao.silva@email.com&DataCadastro=2025-01-02T10:00:00&Ativo=true&__RequestVerificationToken=ABC123
```

---

#### 1.5 Formulário de Edição
```http
GET /Clientes/Edit/{id}
```

**Descrição**: Exibe formulário preenchido para editar cliente.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do cliente |

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Formulário preenchido com dados do cliente

**Exemplo de Uso**:
```http
GET http://localhost:5031/Clientes/Edit/1
```

---

#### 1.6 Editar Cliente
```http
POST /Clientes/Edit/{id}
```

**Descrição**: Atualiza dados de um cliente existente.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do cliente |

**Headers**:
```
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
| Campo | Tipo | Obrigatório | Descrição |
|-------|------|-------------|-----------|
| `IdCliente` | int | ✅ | ID do cliente |
| `Nome` | string | ✅ | Nome do cliente |
| `Sobrenome` | string | ✅ | Sobrenome do cliente |
| `Email` | string | ✅ | Email do cliente |
| `DataCadastro` | datetime | ✅ | Data de cadastro |
| `Ativo` | bool | ✅ | Status ativo/inativo |
| `__RequestVerificationToken` | string | ✅ | Token anti-CSRF |

**Exemplo de Body**:
```
IdCliente=1&Nome=João&Updated&Sobrenome=Silva&Email=joao.silva@email.com&DataCadastro=2025-01-02T10:00:00&Ativo=true&__RequestVerificationToken=...
```

**Resposta**:
- **Sucesso**: `302 Redirect` para `/Clientes`
- **Erro de Validação**: `200 OK` com formulário e erros
- **Cliente não encontrado**: `404 Not Found`
- **Erro de concorrência**: `404 Not Found` ou exceção

**Exemplo de Uso**:
```http
POST http://localhost:5031/Clientes/Edit/1
Content-Type: application/x-www-form-urlencoded

IdCliente=1&Nome=João&Updated&Sobrenome=Silva&Email=joao.silva@email.com&DataCadastro=2025-01-02T10:00:00&Ativo=true&__RequestVerificationToken=ABC123
```

---

#### 1.7 Confirmação de Exclusão
```http
GET /Clientes/Delete/{id}
```

**Descrição**: Exibe confirmação antes de excluir cliente.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do cliente |

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página de confirmação com dados do cliente

**Exemplo de Uso**:
```http
GET http://localhost:5031/Clientes/Delete/1
```

---

#### 1.8 Excluir Cliente
```http
POST /Clientes/Delete/{id}
```

**Descrição**: Exclui um cliente do sistema.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do cliente |

**Headers**:
```
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
| Campo | Tipo | Obrigatório | Descrição |
|-------|------|-------------|-----------|
| `__RequestVerificationToken` | string | ✅ | Token anti-CSRF |

**Resposta**:
- **Sucesso**: `302 Redirect` para `/Clientes`
- **Cliente não encontrado**: `404 Not Found`

**Exemplo de Uso**:
```http
POST http://localhost:5031/Clientes/Delete/1
Content-Type: application/x-www-form-urlencoded

__RequestVerificationToken=ABC123
```

### 2. Produtos

#### 2.1 Listar Produtos
```http
GET /Produto
```

**Descrição**: Retorna uma lista de todos os produtos com informações dos clientes.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página com tabela de produtos e clientes

**Exemplo de Uso**:
```http
GET http://localhost:5031/Produto
```

---

#### 2.2 Detalhes do Produto
```http
GET /Produto/Details/{id}
```

**Descrição**: Exibe detalhes de um produto específico com informações do cliente.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do produto |

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página com detalhes do produto e cliente

**Exemplo de Uso**:
```http
GET http://localhost:5031/Produto/Details/1
```

---

#### 2.3 Formulário de Criação
```http
GET /Produto/Create
```

**Descrição**: Exibe formulário para criar novo produto com lista de clientes.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Formulário de criação com select de clientes

**Exemplo de Uso**:
```http
GET http://localhost:5031/Produto/Create
```

---

#### 2.4 Criar Produto
```http
POST /Produto/Create
```

**Descrição**: Cria um novo produto associado a um cliente.

**Headers**:
```
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
| Campo | Tipo | Obrigatório | Descrição |
|-------|------|-------------|-----------|
| `Nome` | string | ✅ | Nome do produto (máx. 100 chars) |
| `Valor` | decimal | ✅ | Valor do produto |
| `Disponivel` | bool | ✅ | Status de disponibilidade |
| `IdCliente` | int | ✅ | ID do cliente proprietário |
| `__RequestVerificationToken` | string | ✅ | Token anti-CSRF |

**Exemplo de Body**:
```
Nome=Notebook Dell&Valor=2500.00&Disponivel=true&IdCliente=1&__RequestVerificationToken=...
```

**Resposta**:
- **Sucesso**: `302 Redirect` para `/Produto`
- **Erro de Validação**: `200 OK` com formulário e erros

**Exemplo de Uso**:
```http
POST http://localhost:5031/Produto/Create
Content-Type: application/x-www-form-urlencoded

Nome=Notebook Dell&Valor=2500.00&Disponivel=true&IdCliente=1&__RequestVerificationToken=ABC123
```

---

#### 2.5 Formulário de Edição
```http
GET /Produto/Edit/{id}
```

**Descrição**: Exibe formulário preenchido para editar produto.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do produto |

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Formulário preenchido com dados do produto

**Exemplo de Uso**:
```http
GET http://localhost:5031/Produto/Edit/1
```

---

#### 2.6 Editar Produto
```http
POST /Produto/Edit/{id}
```

**Descrição**: Atualiza dados de um produto existente.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do produto |

**Headers**:
```
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
| Campo | Tipo | Obrigatório | Descrição |
|-------|------|-------------|-----------|
| `IdProduto` | int | ✅ | ID do produto |
| `Nome` | string | ✅ | Nome do produto |
| `Valor` | decimal | ✅ | Valor do produto |
| `Disponivel` | bool | ✅ | Status de disponibilidade |
| `IdCliente` | int | ✅ | ID do cliente proprietário |
| `__RequestVerificationToken` | string | ✅ | Token anti-CSRF |

**Exemplo de Body**:
```
IdProduto=1&Nome=Notebook Dell&Updated&Valor=2800.00&Disponivel=true&IdCliente=1&__RequestVerificationToken=...
```

**Resposta**:
- **Sucesso**: `302 Redirect` para `/Produto`
- **Erro de Validação**: `200 OK` com formulário e erros

**Exemplo de Uso**:
```http
POST http://localhost:5031/Produto/Edit/1
Content-Type: application/x-www-form-urlencoded

IdProduto=1&Nome=Notebook Dell&Updated&Valor=2800.00&Disponivel=true&IdCliente=1&__RequestVerificationToken=ABC123
```

---

#### 2.7 Confirmação de Exclusão
```http
GET /Produto/Delete/{id}
```

**Descrição**: Exibe confirmação antes de excluir produto.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do produto |

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página de confirmação com dados do produto

**Exemplo de Uso**:
```http
GET http://localhost:5031/Produto/Delete/1
```

---

#### 2.8 Excluir Produto
```http
POST /Produto/Delete/{id}
```

**Descrição**: Exclui um produto do sistema.

**Parâmetros**:
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `id` | int | ✅ | ID do produto |

**Headers**:
```
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
| Campo | Tipo | Obrigatório | Descrição |
|-------|------|-------------|-----------|
| `__RequestVerificationToken` | string | ✅ | Token anti-CSRF |

**Resposta**:
- **Sucesso**: `302 Redirect` para `/Produto`
- **Produto não encontrado**: `404 Not Found`

**Exemplo de Uso**:
```http
POST http://localhost:5031/Produto/Delete/1
Content-Type: application/x-www-form-urlencoded

__RequestVerificationToken=ABC123
```

### 3. Páginas Gerais

#### 3.1 Página Inicial
```http
GET /
GET /Home
GET /Home/Index
```

**Descrição**: Página inicial da aplicação.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página de boas-vindas

**Exemplo de Uso**:
```http
GET http://localhost:5031/
GET http://localhost:5031/Home
GET http://localhost:5031/Home/Index
```

---

#### 3.2 Página de Privacidade
```http
GET /Home/Privacy
```

**Descrição**: Página de informações de privacidade.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Política de privacidade

**Exemplo de Uso**:
```http
GET http://localhost:5031/Home/Privacy
```

---

#### 3.3 Página de Erro
```http
GET /Home/Error
```

**Descrição**: Página de tratamento de erros.

**Resposta**:
- **Tipo**: HTML View
- **Conteúdo**: Página de erro com detalhes

**Exemplo de Uso**:
```http
GET http://localhost:5031/Home/Error
```

## 🔒 Segurança

### Tokens Anti-CSRF

Todas as operações POST requerem um token anti-CSRF:

```html
<!-- Obter token -->
<input name="__RequestVerificationToken" type="hidden" value="@Html.AntiForgeryToken()" />
```

### Headers de Segurança

```http
X-Requested-With: XMLHttpRequest
__RequestVerificationToken: {token}
```

## 📊 Códigos de Status HTTP

| Código | Descrição | Quando Ocorre |
|--------|-----------|---------------|
| `200 OK` | Sucesso | Operação bem-sucedida |
| `302 Found` | Redirect | Após operações de criação/edição/exclusão |
| `400 Bad Request` | Erro de validação | Dados inválidos enviados |
| `404 Not Found` | Não encontrado | Recurso não existe |
| `500 Internal Server Error` | Erro interno | Erro no servidor |

## 🧪 Testando a API

### Usando cURL

#### Criar Cliente
```bash
# Primeiro, obter o token CSRF
TOKEN=$(curl -s http://localhost:5031/Clientes/Create | grep -o 'name="__RequestVerificationToken" value="[^"]*"' | cut -d'"' -f4)

# Criar cliente
curl -X POST http://localhost:5031/Clientes/Create \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "Nome=João&Sobrenome=Silva&Email=joao@email.com&DataCadastro=2025-01-02T10:00:00&Ativo=true&__RequestVerificationToken=$TOKEN"
```

#### Listar Clientes
```bash
curl http://localhost:5031/Clientes
```

### Usando Postman

1. **Configurar Collection**:
   - Base URL: `http://localhost:5031`
   - Headers: `Content-Type: application/x-www-form-urlencoded`

2. **Obter Token CSRF**:
   - GET `/Clientes/Create`
   - Extrair token da resposta HTML

3. **Configurar Requests**:
   - Incluir token em todos os POSTs
   - Usar form-urlencoded para body

## 📝 Exemplos de Fluxo Completo

### Fluxo: Criar Cliente e Produto

```bash
# 1. Criar cliente
curl -X POST http://localhost:5031/Clientes/Create \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "Nome=Maria&Sobrenome=Santos&Email=maria@email.com&DataCadastro=2025-01-02T10:00:00&Ativo=true&__RequestVerificationToken=$TOKEN"

# 2. Listar clientes para obter ID
curl http://localhost:5031/Clientes

# 3. Criar produto (assumindo cliente ID = 1)
curl -X POST http://localhost:5031/Produto/Create \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "Nome=Smartphone&Valor=1200.00&Disponivel=true&IdCliente=1&__RequestVerificationToken=$TOKEN"

# 4. Listar produtos
curl http://localhost:5031/Produto
```

## 🚀 Próximos Passos

Para implementar uma API REST completa:

1. **Criar Controllers de API**
2. **Implementar JSON Serialization**
3. **Adicionar Autenticação JWT**
4. **Implementar Swagger/OpenAPI**
5. **Adicionar Versionamento de API**
6. **Implementar Rate Limiting**
