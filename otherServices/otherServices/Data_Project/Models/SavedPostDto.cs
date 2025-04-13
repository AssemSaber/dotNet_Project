namespace otherServices.Data_Project.Models
{
    public class SavedPostDto
    {
        public long TenantId { get; set; }
        public long PostId { get; set; }
        public DateTime? DateSaved { get; set; }

        public  PostDTo Post { get; set; }   // You need a PostDto
        public  UserDto Tenant { get; set; } // You need a UserDto

    }
}
