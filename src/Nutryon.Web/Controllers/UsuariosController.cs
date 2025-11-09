
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Nutryon.Web.ViewModels;
using Nutryon.Application.Users;

namespace Nutryon.Web.Controllers;

[Route("Usuarios")] 
public class UsuariosController : Controller
{
    private readonly ListUsuariosUseCase _list;
    private readonly CreateUsuarioUseCase _create;
    private readonly UpdateUsuarioUseCase _update;
    private readonly DeleteUsuarioUseCase _delete;

    public UsuariosController(ListUsuariosUseCase list, CreateUsuarioUseCase create, UpdateUsuarioUseCase update, DeleteUsuarioUseCase delete)
    { _list=list; _create=create; _update=update; _delete=delete; }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _list.Handle(ct));

    [HttpGet("Create")]
    public IActionResult Create() => View(new UsuarioVM());

    [HttpPost("Create")]
    public async Task<IActionResult> Create(UsuarioVM vm, CancellationToken ct)
    {
        if(!ModelState.IsValid) return View(vm);
        await _create.Handle(new UsuarioCreateDto(vm.NomeUsuario, vm.Email, vm.DtNasc is null ? null : DateOnly.FromDateTime(vm.DtNasc.Value)), ct);
        TempData["msg"] = "Usuário criado com sucesso";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:long}")]
    public IActionResult Edit(long id)
    {
        TempData["EditId"] = id;
        return View(new UsuarioVM{ IdUsuario = id });
    }

    [HttpPost("Edit/{id:long}")]
    public async Task<IActionResult> Edit(long id, UsuarioVM vm, CancellationToken ct)
    {
        if(!ModelState.IsValid) return View(vm);
        await _update.Handle(id, new UsuarioUpdateDto(vm.NomeUsuario, vm.Email, vm.DtNasc is null ? null : DateOnly.FromDateTime(vm.DtNasc.Value)), ct);
        TempData["msg"] = "Usuário atualizado";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        await _delete.Handle(id, ct);
        TempData["msg"] = "Usuário removido";
        return RedirectToAction(nameof(Index));
    }
}
