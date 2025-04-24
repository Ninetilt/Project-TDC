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

        public void AddListMember(long listId, long userId)
        {
            if (IsMember(listId, userId)) { return; }
            AddMemberToFile(listId, userId);
            //TO-DO: check in domain logic if user is member already
        }
        public void RemoveListMember(long listId, long userId)
        {
            RemoveMemberFromFile(listId, userId);
        }
        public List<long> GetListMembers(long listId)
        {
            return GetListMembersFromFile(listId);
        }

        #region privates

        private bool IsMember(long listId, long userId)
        {
            //can be removed with sql implementation
            var members = GetAllMembers();
            return members.Any(member => member.ListId == listId && member.UserId == userId);
        } 

        private void AddMemberToFile(long listId, long userId)
        {
            var members = GetAllMembers();
            members.Add(new ListMemberDbo(listId, userId));
            SaveAllMembers(members);
        }

        private void RemoveMemberFromFile(long listId, long userId)
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

        private List<long> GetListMembersFromFile(long listId)
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
            return new ListMemberDbo(long.Parse(elements[0]), long.Parse(elements[1]));
        }
        #endregion
    }
}
