
using Microsoft.AspNetCore.Mvc;
using Nutryon.Web.ViewModels;
using Nutryon.Application.Refeicoes;

namespace Nutryon.Web.Controllers;

[Route("Refeicoes")]
public class RefeicoesController : Controller
{
    private readonly CreateRefeicaoComItensUseCase _create;
    private readonly ListarRefeicoesPorDiaUseCase _listar;
    public RefeicoesController(CreateRefeicaoComItensUseCase c, ListarRefeicoesPorDiaUseCase l) { _create=c; _listar=l; }

    [HttpGet("{idUsuario:long}")] 
    public async Task<IActionResult> Index(long idUsuario, DateTime? data, CancellationToken ct)
    {
        var d = data?.Date ?? DateTime.Today;
        var result = await _listar.Handle(idUsuario, DateOnly.FromDateTime(d), ct);
        ViewBag.IdUsuario = idUsuario;
        ViewBag.Data = d.ToString("yyyy-MM-dd");
        return View(result);
    }

    [HttpGet("Create")] 
    public IActionResult Create(long idUsuario)
        => View(new RefeicaoVM { IdUsuario = idUsuario });

    [HttpPost("Create")]
    public async Task<IActionResult> Create(RefeicaoVM vm, CancellationToken ct)
    {
        if(!ModelState.IsValid) return View(vm);
        var dto = new RefeicaoCreateDto
        {
            IdUsuario = vm.IdUsuario,
            IdTipoRefeicao = vm.IdTipoRefeicao,
            DtRef = DateOnly.FromDateTime(vm.DtRef),
            Observacao = vm.Observacao,
            Itens = new List<RefeicaoItemCreateDto>() // simplificado; itens via outra tela
        };
        await _create.Handle(dto, ct);
        TempData["msg"] = "Refeição criada";
        return RedirectToAction(nameof(Index), new { idUsuario = vm.IdUsuario, data = vm.DtRef.ToString("yyyy-MM-dd") });
    }
}
