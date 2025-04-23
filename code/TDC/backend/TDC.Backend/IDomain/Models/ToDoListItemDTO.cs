namespace TDC.Backend.IDomain.Models;

public record ToDoListItemDto(string description, bool isDone, List<long> finishedMembers, uint effort);
