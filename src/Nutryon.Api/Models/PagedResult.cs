
namespace Nutryon.Api.Models;

public class PagedResult<T>
{
    public required IEnumerable<T> Items { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public object _links { get; set; } = new { };
}
