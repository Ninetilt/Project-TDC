using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListMemberRepository : IListMemberRepository
    {
        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;

        public ListMemberRepository()
        {
            filePath = Path.Combine(projectPath, "Data/list-members.csv");
        }

        public void AddListMember(long listId, string userId)
        {
            //TO-DO: check rather in domain logic if user is member already
            if (IsMember(listId, userId)) { return; }
            AddMemberToFile(listId, userId);
            //TO-DO: move logic to domain or change logic to no status entry = status is false
            AddItemStatusEntryForNewMember(listId, userId);

            
        }
        public void RemoveListMember(long listId, string userId)
        {
            RemoveMemberFromFile(listId, userId);
        }
        public List<string> GetListMembers(long listId)
        {
            return GetListMembersFromFile(listId);
        }

        public List<long> GetListsForUser(string userId)
        {
            return GetUserListsFromFile(userId);
        }

        #region privates

        private void AddItemStatusEntryForNewMember(long listId, string memberId)
        {
            var itemRepos = new ListItemRepository();
            itemRepos.AddItemStatusForNewMember(listId, memberId);
        }

        private List<long> GetUserListsFromFile(string userId)
        {
            var members = GetAllMembers();
            return (from member in members where member.UserId == userId select member.ListId).ToList();
        }

        private bool IsMember(long listId, string userId)
        {
            //can be removed with sql implementation
            var members = GetAllMembers();
            return members.Any(member => member.ListId == listId && member.UserId == userId);
        } 

        private void AddMemberToFile(long listId, string userId)
        {
            var members = GetAllMembers();
            members.Add(new ListMemberDbo(listId, userId));
            SaveAllMembers(members);
        }

        private void RemoveMemberFromFile(long listId, string userId)
        {
            var members = GetAllMembers();
            var newMembers = members.Where(member => member.ListId != listId || member.UserId != userId).ToList();
            SaveAllMembers(newMembers);
        }

        private void SaveAllMembers(List<ListMemberDbo> members)
        {
            var writer = new StreamWriter(filePath);
            foreach (var member in members)
            {
                writer.WriteLine(DboToCsvString(member));
            }
        }

        private static string DboToCsvString(ListMemberDbo dbo)
        {
            return dbo.ListId + ";" + dbo.UserId;
        }

        private List<string> GetListMembersFromFile(long listId)
        {
            var members = GetAllMembers();
            return (from member in members where member.ListId == listId select member.UserId).ToList();
        }

        private List<ListMemberDbo> GetAllMembers()
        {
            var reader = new StreamReader(filePath);
            var ret = new List<ListMemberDbo>();
            while (reader.ReadLine() is { } line)
            {
                ret.Add(ParseToDbo(line));
            }
            return ret;
        }

        private static ListMemberDbo ParseToDbo(string line)
        {
            var elements = line.Split(";");
            return new ListMemberDbo(long.Parse(elements[0]), elements[1]);
        }
        #endregion
    }
}
