namespace Nutryon.Application.Users;

public record UsuarioCreateDto(string NomeUsuario, string Email, DateOnly? DtNasc);
public record UsuarioUpdateDto(string NomeUsuario, string Email, DateOnly? DtNasc);
public record UsuarioReadDto(long IdUsuario, string NomeUsuario, string Email);