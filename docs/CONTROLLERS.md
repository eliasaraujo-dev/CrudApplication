# Documentação dos Controllers - CrudApplication

## 📋 Visão Geral

Este documento detalha os controllers do CrudApplication, suas ações, rotas, validações e funcionalidades CRUD implementadas.

## 🎯 Controllers Implementados

### 1. ClientesController

Controller responsável por gerenciar todas as operações relacionadas aos clientes.

#### Estrutura Base
```csharp
public class ClientesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClientesController(ApplicationDbContext context)
    {
        _context = context;
    }
}
```

#### Ações Implementadas

| Método HTTP | Rota | Ação | Descrição |
|-------------|------|------|-----------|
| GET | `/Clientes` | `Index()` | Lista todos os clientes |
| GET | `/Clientes/Details/{id}` | `Details(int? id)` | Exibe detalhes de um cliente |
| GET | `/Clientes/Create` | `Create()` | Exibe formulário de criação |
| POST | `/Clientes/Create` | `Create([Bind] Cliente cliente)` | Processa criação do cliente |
| GET | `/Clientes/Edit/{id}` | `Edit(int? id)` | Exibe formulário de edição |
| POST | `/Clientes/Edit/{id}` | `Edit(int id, [Bind] Cliente cliente)` | Processa edição do cliente |
| GET | `/Clientes/Delete/{id}` | `Delete(int? id)` | Exibe confirmação de exclusão |
| POST | `/Clientes/Delete/{id}` | `DeleteConfirmed(int id)` | Processa exclusão do cliente |

#### Detalhamento das Ações

##### 1. Index() - Listagem de Clientes
```csharp
public async Task<IActionResult> Index()
{
    return View(await _context.Clientes.ToListAsync());
}
```
- **Função**: Lista todos os clientes cadastrados
- **Retorno**: View com lista de clientes
- **Características**: Operação assíncrona para melhor performance

##### 2. Details(int? id) - Detalhes do Cliente
```csharp
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var cliente = await _context.Clientes
        .FirstOrDefaultAsync(m => m.IdCliente == id);
    if (cliente == null)
    {
        return NotFound();
    }

    return View(cliente);
}
```
- **Função**: Exibe detalhes de um cliente específico
- **Validações**: Verifica se ID foi fornecido e se cliente existe
- **Tratamento de Erro**: Retorna NotFound() para IDs inválidos

##### 3. Create() - Formulário de Criação
```csharp
public IActionResult Create()
{
    return View();
}
```
- **Função**: Exibe formulário para criar novo cliente
- **Retorno**: View com formulário vazio

##### 4. Create([Bind] Cliente cliente) - Processamento de Criação
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("IdCliente,Nome,Sobrenome,Email,DataCadastro,Ativo")] Cliente cliente)
{
    if (ModelState.IsValid)
    {
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(cliente);
}
```
- **Função**: Processa criação de novo cliente
- **Segurança**: ValidateAntiForgeryToken para proteção CSRF
- **Validação**: ModelState.IsValid para validação de dados
- **Bind**: Proteção contra overposting
- **Fluxo**: Redireciona para Index após sucesso

##### 5. Edit(int? id) - Formulário de Edição
```csharp
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var cliente = await _context.Clientes.FindAsync(id);
    if (cliente == null)
    {
        return NotFound();
    }
    return View(cliente);
}
```
- **Função**: Exibe formulário preenchido para edição
- **Validações**: Verifica ID e existência do cliente
- **Carregamento**: Busca cliente pelo ID

##### 6. Edit(int id, [Bind] Cliente cliente) - Processamento de Edição
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nome,Sobrenome,Email,DataCadastro,Ativo")] Cliente cliente)
{
    if (id != cliente.IdCliente)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(cliente);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExists(cliente.IdCliente))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(cliente);
}
```
- **Função**: Processa edição do cliente
- **Validações**: Verifica consistência de ID e ModelState
- **Tratamento de Erro**: Captura DbUpdateConcurrencyException
- **Concorrência**: Verifica se cliente ainda existe

##### 7. Delete(int? id) - Confirmação de Exclusão
```csharp
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var cliente = await _context.Clientes
        .FirstOrDefaultAsync(m => m.IdCliente == id);
    if (cliente == null)
    {
        return NotFound();
    }

    return View(cliente);
}
```
- **Função**: Exibe confirmação antes da exclusão
- **Segurança**: Permite visualizar dados antes de excluir

##### 8. DeleteConfirmed(int id) - Processamento de Exclusão
```csharp
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var cliente = await _context.Clientes.FindAsync(id);
    if (cliente != null)
    {
        _context.Clientes.Remove(cliente);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
```
- **Função**: Processa exclusão do cliente
- **Segurança**: ActionName("Delete") para evitar conflitos de rota
- **Verificação**: Verifica se cliente existe antes de remover

##### 9. ClienteExists(int id) - Método Auxiliar
```csharp
private bool ClienteExists(int id)
{
    return _context.Clientes.Any(e => e.IdCliente == id);
}
```
- **Função**: Verifica se cliente existe no banco
- **Uso**: Utilizado para validações de existência

### 2. ProdutoController

Controller responsável por gerenciar todas as operações relacionadas aos produtos.

#### Estrutura Base
```csharp
public class ProdutoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProdutoController(ApplicationDbContext context)
    {
        _context = context;
    }
}
```

#### Ações Implementadas

| Método HTTP | Rota | Ação | Descrição |
|-------------|------|------|-----------|
| GET | `/Produto` | `Index()` | Lista todos os produtos com clientes |
| GET | `/Produto/Details/{id}` | `Details(int? id)` | Exibe detalhes de um produto |
| GET | `/Produto/Create` | `Create()` | Exibe formulário de criação |
| POST | `/Produto/Create` | `Create([Bind] Produto produto)` | Processa criação do produto |
| GET | `/Produto/Edit/{id}` | `Edit(int? id)` | Exibe formulário de edição |
| POST | `/Produto/Edit/{id}` | `Edit(int id, [Bind] Produto produto)` | Processa edição do produto |
| GET | `/Produto/Delete/{id}` | `Delete(int? id)` | Exibe confirmação de exclusão |
| POST | `/Produto/Delete/{id}` | `DeleteConfirmed(int id)` | Processa exclusão do produto |

#### Características Especiais do ProdutoController

##### 1. Index() - Listagem com Relacionamentos
```csharp
public async Task<IActionResult> Index()
{
    var applicationDbContext = _context.Produtos.Include(p => p.Cliente);
    return View(await applicationDbContext.ToListAsync());
}
```
- **Diferencial**: Carrega produtos com informações do cliente
- **Include()**: Carregamento explícito do relacionamento
- **Performance**: Evita N+1 queries

##### 2. Create() - Formulário com SelectList
```csharp
public IActionResult Create()
{
    ViewBag.IdCliente = new SelectList(_context.Clientes.OrderBy(c => c.Nome), "IdCliente", "Nome");
    return View();
}
```
- **Diferencial**: Carrega lista de clientes para seleção
- **ViewBag**: Passa SelectList para a view
- **Ordenação**: Clientes ordenados por nome

##### 3. Create([Bind] Produto produto) - Processamento com Relacionamento
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("IdProduto,Nome,Valor,Disponivel,IdCliente")] Produto produto)
{
    if (ModelState.IsValid)
    {
        _context.Add(produto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Email", produto.IdCliente);
    return View(produto);
}
```
- **Diferencial**: Recarrega SelectList em caso de erro
- **ViewData**: Alternativa ao ViewBag para SelectList

##### 4. Edit(int? id) - Formulário com SelectList Atualizado
```csharp
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var produto = await _context.Produtos.FindAsync(id);
    if (produto == null)
    {
        return NotFound();
    }
    ViewBag.IdCliente = new SelectList(_context.Clientes.OrderBy(c => c.Nome), "IdCliente", "Nome", produto.IdCliente);
    return View(produto);
}
```
- **Diferencial**: SelectList com valor selecionado
- **Pré-seleção**: Cliente atual do produto é selecionado

##### 5. Edit([Bind] Produto produto) - Processamento com SelectList
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("IdProduto,Nome,Valor,Disponivel,IdCliente")] Produto produto)
{
    if (id != produto.IdProduto)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(produto.IdProduto))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    ViewBag.IdCliente = new SelectList(_context.Clientes.OrderBy(c => c.Nome), "IdCliente", "Nome", produto.IdCliente);
    return View(produto);
}
```
- **Diferencial**: Recarrega SelectList em caso de erro de validação

##### 6. Delete(int? id) - Exclusão com Relacionamento
```csharp
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var produto = await _context.Produtos
        .Include(p => p.Cliente)
        .FirstOrDefaultAsync(m => m.IdProduto == id);
    if (produto == null)
    {
        return NotFound();
    }

    return View(produto);
}
```
- **Diferencial**: Carrega produto com informações do cliente
- **Include()**: Para exibir dados do cliente na confirmação

### 3. HomeController

Controller padrão para páginas iniciais e de erro.

```csharp
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
```

## 🛡️ Padrões de Segurança Implementados

### 1. Proteção CSRF
```csharp
[ValidateAntiForgeryToken]
```
- **Aplicação**: Todas as ações POST
- **Função**: Previne ataques Cross-Site Request Forgery

### 2. Proteção Overposting
```csharp
[Bind("IdCliente,Nome,Sobrenome,Email,DataCadastro,Ativo")]
```
- **Aplicação**: Ações Create e Edit
- **Função**: Especifica quais propriedades podem ser alteradas

### 3. Validação de Modelo
```csharp
if (ModelState.IsValid)
{
    // Processa operação
}
```
- **Aplicação**: Todas as ações POST
- **Função**: Valida dados antes de processar

## 🔄 Padrões de Fluxo

### Fluxo Típico CRUD

#### Create
1. GET `/Controller/Create` → Exibe formulário
2. POST `/Controller/Create` → Valida e salva
3. Redirect para Index

#### Read
1. GET `/Controller` → Lista todos
2. GET `/Controller/Details/{id}` → Exibe detalhes

#### Update
1. GET `/Controller/Edit/{id}` → Exibe formulário preenchido
2. POST `/Controller/Edit/{id}` → Valida e atualiza
3. Redirect para Index

#### Delete
1. GET `/Controller/Delete/{id}` → Exibe confirmação
2. POST `/Controller/Delete/{id}` → Confirma e remove
3. Redirect para Index

## 📊 Tratamento de Erros

### Tipos de Erro Tratados

1. **ID Nulo**:
   ```csharp
   if (id == null)
   {
       return NotFound();
   }
   ```

2. **Entidade Não Encontrada**:
   ```csharp
   if (cliente == null)
   {
       return NotFound();
   }
   ```

3. **Erro de Concorrência**:
   ```csharp
   catch (DbUpdateConcurrencyException)
   {
       if (!ClienteExists(cliente.IdCliente))
       {
           return NotFound();
       }
       else
       {
           throw;
       }
   }
   ```

4. **Inconsistência de ID**:
   ```csharp
   if (id != cliente.IdCliente)
   {
       return NotFound();
   }
   ```

## 🚀 Otimizações Implementadas

### 1. Operações Assíncronas
```csharp
public async Task<IActionResult> Index()
{
    return View(await _context.Clientes.ToListAsync());
}
```

### 2. Carregamento Explícito
```csharp
var applicationDbContext = _context.Produtos.Include(p => p.Cliente);
```

### 3. Consultas Otimizadas
```csharp
var cliente = await _context.Clientes.FindAsync(id);
```

## 📝 Exemplos de Uso

### Chamada para Listagem
```http
GET /Clientes
```

### Chamada para Detalhes
```http
GET /Clientes/Details/1
```

### Chamada para Criação
```http
POST /Clientes/Create
Content-Type: application/x-www-form-urlencoded

Nome=João&Sobrenome=Silva&Email=joao@email.com&DataCadastro=2025-01-02&Ativo=true&__RequestVerificationToken=...
```

### Chamada para Edição
```http
POST /Clientes/Edit/1
Content-Type: application/x-www-form-urlencoded

IdCliente=1&Nome=João&Updated&Sobrenome=Silva&Email=joao@email.com&DataCadastro=2025-01-02&Ativo=true&__RequestVerificationToken=...
```

### Chamada para Exclusão
```http
POST /Clientes/Delete/1
Content-Type: application/x-www-form-urlencoded

__RequestVerificationToken=...
```

