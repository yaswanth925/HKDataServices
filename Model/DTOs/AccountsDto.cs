using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class AccountsDto
    {
        public Guid AccountID { get; set; }
        [Required]
        public int DealerCode { get; set; }
        [Required]
        public string DealerName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string GSTNumber { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Sales { get; set; }
        public DateTime? Date { get; set; }
        public IFormFile? FileData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

    }
}
