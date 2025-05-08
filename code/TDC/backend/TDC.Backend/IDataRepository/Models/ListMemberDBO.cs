namespace TDC.Backend.IDataRepository.Models
{
    public class ListMemberDbo
    {
        public long ListId { get; set; }
        public string Username { get; set; }
        public bool IsCreator { get; set; }

        public ListMemberDbo() { }

        public ListMemberDbo(long listId, string username, bool isCreator)
        {
            ListId = listId;
            Username = username;
            IsCreator = isCreator;
        }
    }
}
