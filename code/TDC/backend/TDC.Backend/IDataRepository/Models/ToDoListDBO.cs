namespace TDC.Backend.IDataRepository.Models;

public record ToDoListDbo(long listId, long userId, string name, bool isCollaborative, bool isFinished);
