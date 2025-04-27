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
            var directoryPath = Path.Combine(projectPath, "TestData");
            if (!Directory.Exists(directoryPath)) { Directory.CreateDirectory(directoryPath); }

            filePath = Path.Combine(directoryPath, "list-members.csv");
            if (!File.Exists(filePath)) { File.WriteAllText(filePath, string.Empty); }
        }

        public void AddListMember(long listId, string userId, bool isCreator)
        {
            AddMemberToFile(listId, userId, isCreator);
        }
        public void RemoveListMember(long listId, string userId)
        {
            RemoveMemberFromFile(listId, userId);
        }

        public bool UserIsCreator(long listId, string username)
        {
            var members = GetAllMembers();
            foreach (var member in members) {
                if(member.UserId.Equals(username) && member.ListId == listId && member.IsCreator) { return true; }
            }
            return false;
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

        private List<long> GetUserListsFromFile(string userId)
        {
            var members = GetAllMembers();
            return (from member in members where member.UserId.Equals(userId) select member.ListId).ToList();
        }

        public bool UserIsMember(long listId, string userId)
        {
            //can be removed with sql implementation
            var members = GetAllMembers();
            return members.Any(member => member.ListId == listId && member.UserId.Equals(userId));
        } 

        private void AddMemberToFile(long listId, string userId, bool isCreator)
        {
            var members = GetAllMembers();
            members.Add(new ListMemberDbo(listId, userId, isCreator));
            SaveAllMembers(members);
        }

        private void RemoveMemberFromFile(long listId, string userId)
        {
            var members = GetAllMembers();
            var newMembers = members.Where(member => member.ListId != listId || !member.UserId.Equals(userId)).ToList();
            SaveAllMembers(newMembers);
        }

        private void SaveAllMembers(List<ListMemberDbo> members)
        {
            var writer = new StreamWriter(filePath);
            foreach (var member in members)
            {
                writer.WriteLine(ParseToCsvLine(member));
            }
            writer.Close();
        }

        private static string ParseToCsvLine(ListMemberDbo dbo)
        {
            return dbo.ListId + ";" + dbo.UserId + ";" + dbo.IsCreator;
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
            reader.Close();
            return ret;
        }

        private static ListMemberDbo ParseToDbo(string line)
        {
            var elements = line.Split(";");
            return new ListMemberDbo(long.Parse(elements[0]), elements[1], bool.Parse(elements[2]));
        }
        #endregion
    }
}
