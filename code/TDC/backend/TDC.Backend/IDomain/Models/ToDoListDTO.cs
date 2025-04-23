namespace TDC.Backend.IDomain.Models;

public record ToDoListDto(long listId, string name, List<ToDoListItemDto> items, List<long> members, bool isCollaborative, bool isFinished);
