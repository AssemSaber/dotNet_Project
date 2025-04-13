namespace otherServices.Data_Project.Models
{
    public class PostDTo
    {
        public required long PostId { get; set; }
        public  string Title { get; set; } // remove all required
        public  string Body { get; set; }
        public  double Price { get; set; }
        public  string Loca { get; set; }
        public  DateTime Date { get; set; }
        public UserDto Landlord { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
