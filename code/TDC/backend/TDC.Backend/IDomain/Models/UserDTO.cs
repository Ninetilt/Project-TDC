namespace TDC.Backend.IDomain.Models;

public record UserDto(string email, string password, string username, string description, long characterId);
