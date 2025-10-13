namespace HKDataServices.Model.DTOs
{
    public class ChangePasswordDto
    {
        public string UserName { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
