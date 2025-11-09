using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nutryon.Application.Users;

namespace Nutryon.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly CreateUsuarioUseCase _create;
    private readonly UpdateUsuarioUseCase _update;
    private readonly DeleteUsuarioUseCase _delete;
    private readonly ListUsuariosUseCase _list;

    public UsuariosController(
        CreateUsuarioUseCase create,
        UpdateUsuarioUseCase update,
        DeleteUsuarioUseCase delete,
        ListUsuariosUseCase list)
        => (_create, _update, _delete, _list) = (create, update, delete, list);

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
        => Ok(await _list.Handle(ct));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioCreateDto dto, CancellationToken ct)
    {
        var id = await _create.Handle(dto, ct);
        return CreatedAtAction(nameof(Post), new { id }, new { id });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, [FromBody] UsuarioUpdateDto dto, CancellationToken ct)
    {
        await _update.Handle(id, dto, ct);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        await _delete.Handle(id, ct);
        return NoContent();
    }
}