namespace API.DTO.Response
{
    public class ProfileResponse
    {
        public int AccountId { get; set; } // Maps to UserId
        public string? UserName { get; set; } // Matches UserName in User entity
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public int RoleId { get; set; } // Added from User entity
        public string Message { get; set; } = string.Empty;
    }
}
