namespace api.DTOs.User
{
    public class ProfileUpdateDTO
    {
        public string? Name { get; set;}
        public string? Address { get; set;}
        public int? Phone { get; set;}
        public Guid? AvatarId { get; set;}
    }
}