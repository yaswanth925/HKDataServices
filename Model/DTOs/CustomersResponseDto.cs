using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class CustomersResponseDto
    {
        public Guid CustomerID { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        [Required]
        public string? EmailId { get; set; }
        [Required]
        public string? GSTNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Pincode { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? ImageBase64 { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string? ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
    }
}
