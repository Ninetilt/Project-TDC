namespace TDC.Backend.IDomain.Models;

public record ToDoListDto(long ListId, string Name, List<ToDoListItemLoadingDto> Items, List<string> Members, bool IsCollaborative);
