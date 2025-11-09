using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Nutryon.Application.Refeicoes;

namespace Nutryon.Api.Controllers;

[ApiController]
[Route("api/usuarios/{idUsuario:long}/refeicoes")]
public class RefeicoesController : ControllerBase
{
    private readonly CreateRefeicaoComItensUseCase _create;
    private readonly ListarRefeicoesPorDiaUseCase _listar;

    public RefeicoesController(CreateRefeicaoComItensUseCase create, ListarRefeicoesPorDiaUseCase listar)
        => (_create, _listar) = (create, listar);

    [HttpPost]
    public async Task<IActionResult> Post(long idUsuario, [FromBody] RefeicaoCreateDto dto, CancellationToken ct)
    {
        dto = dto with { IdUsuario = idUsuario };
        var id = await _create.Handle(dto, ct);
        return CreatedAtAction(nameof(Post), new { idUsuario, id }, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Get(long idUsuario, [FromQuery] DateOnly data, CancellationToken ct)
        => Ok(await _listar.Handle(idUsuario, data, ct));
}