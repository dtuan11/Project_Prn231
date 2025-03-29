namespace API.DTO.Request
{
    public class ProfileRequest
    {
        public int AccountId { get; set; } 
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public IFormFile? AvatarFile { get; set; }
    }
}
