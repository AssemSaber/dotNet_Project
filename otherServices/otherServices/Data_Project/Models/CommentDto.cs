namespace otherServices.Data_Project.Models
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public long PostId { get; set; }
        public string comment_written { get; set; }
        public DateTime DateComment { get; set; }
    }
}
