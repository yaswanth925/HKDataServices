using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class UsersFormDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string MobileNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string EmailID { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Createdby { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}