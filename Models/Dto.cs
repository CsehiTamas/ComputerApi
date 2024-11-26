using ZstdSharp.Unsafe;

namespace ComputerApi.Models
{
    public record CreateOsDto(string? Name);
    public record UpdateOsDto(string? Name);

    public record CreateCompDto(string? Brand, string? Type, double Display, int? Memory, Guid? OsId);

    public record UpdateCombDto(string? Brand, string? Type, double Display, int? Memory);


}
