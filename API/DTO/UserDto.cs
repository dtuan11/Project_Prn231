namespace API.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool Active { get; set; }
    }
}
