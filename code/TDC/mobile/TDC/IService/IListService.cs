using TDC.Models;
using TDC.Models.DTOs;

namespace TDC.IService;
public interface IListService
{
    public Task<long> CreateList(string name, bool isCollaborative, string creator);
    public Task UpdateListTitle(string newTitle, long listId);
    public Task DeleteList(long listId, string sender);
    public Task FinishList(long listId, string sender);
    public Task<ToDoList> GetListById(long listId);
    public Task<List<ToDoList>> GetAllListsForUser(string username);
    public Task<List<RewardingMessageDto>> GetOpenRewardsForUser(string username);
    public Task RemoveSeenReward(string username, long listId);
    public Task<ListMembersDto> GetMembersForList(long listId);
    public Task<int> GetPointsForMember(string username, long listId);
    public Task AddUserToList(string username, long listId);
    public Task<bool> IsUserCreator(string username, long listId);
    public Task RemoveUserFromList(string username, long listId);
}

