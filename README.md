# 📦 CrudMvc

Aplicação Web desenvolvida com **ASP.NET Core 10 MVC** e **MySQL**, implementando um sistema completo de **CRUD** (Create, Read, Update, Delete) para gerenciamento de Departamentos, Vendedores e Registros de Vendas.

---

## 📌 O que é MVC?

**MVC** (Model-View-Controller) é um padrão de arquitetura de software que separa a aplicação em três camadas com responsabilidades distintas:

| Camada | Responsabilidade |
|--------|-----------------|
| **Model** | Representa os dados e as regras de negócio da aplicação. É onde ficam as classes de entidade, ViewModels e a lógica de acesso ao banco de dados. |
| **View** | Responsável pela interface visual apresentada ao usuário. No ASP.NET, as Views são arquivos `.cshtml` que combinam HTML com C# (Razor). |
| **Controller** | Atua como intermediário entre a Model e a View. Recebe as requisições do usuário, processa a lógica necessária chamando os Services, e retorna a View adequada com os dados. |

### Fluxo de uma requisição MVC:
```
Usuário → Controller → Service → Model (banco de dados) → Controller → View → Usuário
```

---

## 🛠️ Tecnologias Utilizadas

### ASP.NET Core 10 MVC
Framework da Microsoft para desenvolvimento de aplicações web. Utiliza o padrão MVC nativamente, com suporte a Razor Pages, injeção de dependência, middlewares e roteamento automático por convenção.

### Entity Framework Core
ORM (Object-Relational Mapper) que permite trabalhar com o banco de dados usando objetos C# em vez de SQL puro. Responsável por:
- Mapear as classes C# para tabelas no banco
- Gerar e executar Migrations (versionamento do banco)
- Realizar queries via LINQ
- Gerenciar relacionamentos entre entidades (FK, navegação, Include)

### MySql.EntityFrameworkCore
Driver oficial da Oracle que conecta o Entity Framework Core ao MySQL. Utilizado por ser compatível com o EF Core 10, já que o Pomelo (alternativa mais popular) ainda não possui suporte para esta versão.

### MySQL 8.x
Banco de dados relacional utilizado para persistência dos dados. Gerenciado localmente via **MySQL Workbench**.

### Bootstrap (Bootswatch Lux)
Framework CSS utilizado para estilização da interface. O tema **Lux** do Bootswatch foi escolhido por sua estética refinada com tipografia elegante. O arquivo está localizado na pasta `wwwroot/lib/bootstrap/dist/css/bootstrap-lux.css`.

### Razor (.cshtml)
Engine de templates do ASP.NET que permite misturar C# com HTML nas Views. Utilizado para renderização dinâmica de dados, formulários com validação e navegação entre páginas.

---

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas, separando responsabilidades de forma clara:

```
CrudMvc/
├── Controllers/          # Recebem requisições e orquestram o fluxo
│   ├── DepartamentsController.cs
│   ├── HomeController.cs
│   ├── SalesRecordsController.cs
│   └── SellersController.cs
│
├── Data/                 # Contexto do Entity Framework
│   └── CrudMvcContext.cs
│
├── Migrations/           # Histórico de versões do banco de dados
│
├── Models/               # Entidades e ViewModels
│   ├── Enums/
│   │   └── SaleStatus.cs
│   ├── ViewModels/
│   │   ├── ErrorViewModel.cs
│   │   ├── SalesRecordFormViewModel.cs
│   │   └── SellerFormViewModel.cs
│   ├── Departament.cs
│   ├── SalesRecord.cs
│   └── Seller.cs
│
├── Services/             # Lógica de negócio
│   ├── Exceptions/
│   │   ├── DbException.cs
│   │   ├── IntegrityException.cs
│   │   └── NotFoundException.cs
│   ├── DepartamentService.cs
│   ├── SalesRecordService.cs
│   └── SeedingService.cs
│
├── Views/                # Interface visual (Razor)
│   ├── Departaments/
│   │   ├── Create.cshtml
│   │   ├── Delete.cshtml
│   │   ├── Details.cshtml
│   │   ├── Edit.cshtml
│   │   └── Index.cshtml
│   ├── Home/
│   ├── SalesRecords/
│   │   ├── Create.cshtml
│   │   ├── GroupingSearch.cshtml
│   │   ├── Index.cshtml
│   │   └── SimpleSearch.cshtml
│   ├── Sellers/
│   │   ├── Create.cshtml
│   │   ├── Delete.cshtml
│   │   ├── Details.cshtml
│   │   ├── Edit.cshtml
│   │   └── Index.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _ViewImports.cshtml
│       └── _ViewStart.cshtml
│
├── wwwroot/              # Arquivos estáticos (CSS, JS, Bootstrap)
├── appsettings.json      # Configurações e connection string
└── Program.cs            # Ponto de entrada e configuração da aplicação
```

---

## 🗄️ Modelagem do Banco de Dados

### Entidades e Relacionamentos

```
Departament (1) ──────── (N) Seller (1) ──────── (N) SalesRecord
```

- Um **Departamento** pode ter vários **Vendedores**
- Um **Vendedor** pertence a apenas um **Departamento**
- Um **Vendedor** pode ter vários **Registros de Venda**
- Cada **Registro de Venda** pertence a um único **Vendedor**

### Diagrama das Entidades

**Departament**
| Campo | Tipo |
|-------|------|
| Id | int (PK) |
| Name | string |

**Seller**
| Campo | Tipo |
|-------|------|
| Id | int (PK) |
| Name | string |
| Email | string |
| BirthDate | DateTime |
| BaseSalary | double |
| DepartamentId | int (FK) |

**SalesRecord**
| Campo | Tipo |
|-------|------|
| Id | int (PK) |
| Date | DateTime |
| Amount | double |
| Status | SaleStatus (Enum) |
| SellerId | int (FK) |

---

## ⚙️ Padrões e Conceitos Utilizados

### Injeção de Dependência
Os Services são registrados no `Program.cs` com `AddScoped` e injetados nos Controllers via construtor, seguindo o princípio de inversão de dependência.

### ViewModels
Usados para passar múltiplos objetos para uma View, como o `SellerFormViewModel` que agrupa o `Seller` e a lista de `Departaments` para popular o select de departamentos no formulário.

### Eager Loading
Utilizado com o método `.Include()` do EF Core para carregar entidades relacionadas em uma única query, evitando o problema de N+1 queries.

```csharp
_context.Seller.Include(s => s.Departament).ToListAsync();
```

### Seeding Service
Popula o banco com dados iniciais automaticamente na primeira execução, caso as tabelas estejam vazias.

### Tratamento de Exceções
Camada de exceções customizadas (`NotFoundException`, `IntegrityException`, `DbException`) para comunicar erros de negócio de forma semântica entre as camadas.

---

## 🚀 Como Rodar o Projeto

### Pré-requisitos
- .NET 10 SDK
- MySQL 8.x instalado
- MySQL Workbench (opcional, recomendado)
- Visual Studio 2022 ou VS Code

### Passo a passo

**1. Clone o repositório**
```bash
git clone https://github.com/seu-usuario/CrudMvc.git
cd CrudMvc
```

**2. Crie o banco de dados no MySQL Workbench**
```sql
CREATE DATABASE crudmvc;
```

**3. Configure a connection string no `appsettings.json`**
```json
"ConnectionStrings": {
  "CrudMvcContext": "Server=localhost;Database=crudmvc;User=root;Password=suasenha;"
}
```

**4. Rode as migrations para criar as tabelas**
```
Update-Database
```

**5. Execute o projeto**
```
dotnet run
```
Ou pressione `F5` no Visual Studio.

> Na primeira execução o **SeedingService** popula automaticamente o banco com dados de exemplo.

---

## ✅ Funcionalidades

- [x] CRUD completo de Departamentos
- [x] CRUD completo de Vendedores
- [x] Registro de Vendas com seleção de Vendedor
- [x] Busca de Vendas por período (Simple Search)
- [x] Busca de Vendas agrupada por Departamento (Grouping Search)
- [x] Proteção de integridade ao deletar Departamento com Vendedores vinculados
- [x] Seeding automático de dados iniciais
- [x] Página de erro customizada

---

## 👨‍💻 Desenvolvido com ASP.NET Core 10 + MySQL
Por Celestino0310
