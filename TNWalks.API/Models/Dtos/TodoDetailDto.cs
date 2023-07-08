using TNWalks.API.Models.Domain;

namespace TNWalks.API.Models.Dtos
{
    public class TodoDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}