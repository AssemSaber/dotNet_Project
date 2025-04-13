namespace otherServices.Data_Project.Models
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public required string FName { get; set; }
        public required string LName { get; set; }
    }
}
