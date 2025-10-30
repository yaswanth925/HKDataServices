using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class UsersResponseDto
    {
        public Guid ID { get; set; }
        [Required]
        public string FirstName { get; set; } 
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
    }
}