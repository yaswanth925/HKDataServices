using HKDataServices.Model;
using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model
{
    public class Customers
    {
        [Key]
        public Guid CustomerID { get; set; } = Guid.NewGuid();

        [Required]
        public string CustomerName { get; set; } = string.Empty;
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? GSTNumber { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Description { get; set; }
        public byte[]? PhotoUpload { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

    }
}
