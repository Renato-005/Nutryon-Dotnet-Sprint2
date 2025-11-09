# Nutryon
- Integrantes: Renato | RM560928 / Victor | RM560087

## Objetivo do Projeto
O Nutryon é um app que ajuda você a planejar o que comer programando suas refeições por dia da semana e calcula automaticamente calorias e macronutrientes (proteínas, carboidratos e gorduras) a partir de um banco de ingredientes cadastrado.

## 1) Camada Web – Web API / Minimal API
- **Rotas de *Search*** com paginação, ordenação e filtros:
  - `GET /api/usuarios/search?q=&page=1&pageSize=10&sortBy=NomeUsuario|Email&sortDir=asc|desc`
  - `GET /api/usuarios/{idUsuario}/refeicoes/search?data=2025-06-10&page=1&pageSize=10`
- **HATEOAS**: as respostas de busca retornam `_links.self`, `_links.next`, `_links.prev`.

## 2) Conexão com banco (Oracle – padrão *Candidatos*)
- `appsettings.json` agora usa `ConnectionStrings:NutryonDb` com o formato:
  ```json
  "User Id=USUARIO;Password=SENHA;Data Source=oracle.fiap.com.br:1521/ORCL;"
  ```
- `Program.cs` foi refeito para `UseOracle(...)` e HealthChecks (`/health/live` e `/health/ready`).

## 3) Pacotes NuGet
No diretório `src/Nutryon.Api` e `src/Nutryon.Infrastructure` execute:
```bash
dotnet restore
```
Os principais pacotes adicionados/substituídos:
- `Oracle.EntityFrameworkCore` (substitui `Microsoft.EntityFrameworkCore.SqlServer`)
- `AspNetCore.HealthChecks.Oracle` e `AspNetCore.HealthChecks.UI.Client`
- `FluentValidation.AspNetCore` (já existia)

## 4) Como rodar
1. Configure o usuário e senha Oracle no `src/Nutryon.Api/appsettings.json` (padrão FIAP).
2. Na raiz do repositório:
   ```bash
   dotnet build
   dotnet run --project src/Nutryon.Api/Nutryon.Api.csproj
   ```
3. Acesse:
   - `http://localhost:5184/swagger`
   - `http://localhost:5184/health/ready`

> **Observação**: como a estrutura de *Views MVC* não existia no projeto original, mantivemos a entrega no formato **Web API** com os itens exigidos (Search + HATEOAS + Controllers CRUD).
## 5) Endpoints principais
- `POST /api/usuarios`
- `GET  /api/usuarios`
- `GET  /api/usuarios/search`
- `PUT  /api/usuarios/{id}`
- `DELETE /api/usuarios/{id}`
- `POST /api/usuarios/{idUsuario}/refeicoes`
- `GET  /api/usuarios/{idUsuario}/refeicoes`
- `GET  /api/usuarios/{idUsuario}/refeicoes/search`

## 6) Camada MVC / Views & Layouts (Bootstrap)
- Projeto **Nutryon.Web (MVC)** adicionado com:
  - **Rotas padrão** (`{controller=Home}/{action=Index}/{id?}`) e páginas principais.
  - **Rotas personalizadas** (links de navegação e controllers).
  - **Layout principal (_Layout.cshtml)** com **Bootstrap** (navbar, container).
  - **Views com validação** (DataAnnotations) e **ViewModels** para isolar a lógica.
  - Telas implementadas: **Home**, **Usuários (Index, Create, Edit)** e **Refeições (Index, Create)**.
