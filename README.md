# Nutryon
- Integrantes: Renato | RM560928 / Victor | RM560087

## Objetivo do Projeto
O Nutryon é um app que ajuda você a planejar o que comer programando suas refeições por dia da semana e calcula automaticamente calorias e macronutrientes (proteínas, carboidratos e gorduras) a partir de um banco de ingredientes cadastrado.

## Escopo
- CRUD de **Usuário**.
- **Refeições**: criação com itens e listagem por data/usuário.
- **Migrações EF Core** com **seed** inicial (tipos de refeição, unidades, ingredientes).
- **Validações** com FluentValidation e **tratamento de erros** em ProblemDetails.

## Requisitos Funcionais
- Cadastrar, atualizar, excluir e listar usuários.
- Registrar refeição com pelo menos 1 item (quantidade > 0).
- Consultar refeições por data para um usuário.

## Requisitos Não Funcionais
- .NET 8, ASP.NET Core Web API.
- Clean Architecture (Domain / Application / Infrastructure / Api).
- EF Core com SQL Server LocalDB.
- AutoMapper, FluentValidation, Swagger.

## Arquitetura (Clean Architecture)
- **Domain**: Entidades e portas (`IUsuarioRepository`, `IRefeicaoRepository`, `IUnitOfWork`).
- **Application**: DTOs + **UseCases**; regras/validações; **MappingProfile**.
- **Infrastructure**: EF Core (`DbContext`), repositórios concretos, **Migrations** + Seed.
- **Api**: Controllers, Swagger, **ProblemDetails** middleware, DI.

## Como rodar
1. Abrir `Nutryon.sln` (VS 2022+). Definir `src/Nutryon.Api` como Startup Project.
2. `F5` e acesse `/swagger`.

### Exemplos
**POST /api/usuarios**
```json
{ "nomeUsuario": "Renato", "email": "renato@example.com", "dtNasc": "2000-05-10" }
```
**POST /api/usuarios/{idUsuario}/refeicoes**
```json
{
  "idTipoRefeicao": 2,
  "dtRef": "2025-01-15",
  "observacao": "Almoço",
  "itens": [
    { "idIngrediente": 1, "qtde": 150, "idUnidade": 1 },
    { "idIngrediente": 3, "qtde": 120, "idUnidade": 1 }
  ]
}
```
**GET /api/usuarios/{idUsuario}/refeicoes?data=2025-01-15**

## Observações
- O seed inicial insere: Tipos (Café, Almoço, Jantar), Unidades (g, ml) e Ingredientes (Arroz, Feijão, Frango).
- Caso deseje Oracle ou outro provider, basta trocar o `UseSqlServer` e os pacotes no projeto Infrastructure.
