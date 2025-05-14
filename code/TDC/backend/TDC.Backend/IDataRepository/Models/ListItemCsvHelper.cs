namespace TDC.Backend.IDataRepository.Models
{
    // TO-DO: only for working with csv -> remove once database is used
    public class ListItemCsvHelper(long itemId, long listId, string description, uint effort)
    {
        public long ItemId { get; set; } = itemId;
        public long ListId { get; set; } = listId;
        public string Description { get; set; } = description;
        public uint Effort { get; set; } = effort;
    }
}

