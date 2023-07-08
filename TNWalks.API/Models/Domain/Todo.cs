namespace TNWalks.API.Models.Domain
{
    public class Todo : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
    }
}