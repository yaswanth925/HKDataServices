namespace HKDataServices.Model.DTOs
{
    public class VerifyOtpDto
    {
        public string UserName { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
