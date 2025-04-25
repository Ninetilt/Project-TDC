namespace TDC.Backend.IDomain.Models;

public record ToDoListDto(long listId, string name, List<ToDoListItemLoadingDto> items, List<long> members, bool isCollaborative, bool isFinished);
