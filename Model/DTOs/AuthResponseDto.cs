namespace HKDataServices.Model.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public UsersResponseDto? User { get; set; }
    }
}
