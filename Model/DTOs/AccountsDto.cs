using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class AccountsDto
    {
        public Guid AccountID { get; set; }
        [Required]
        public int DealerCode { get; set; }
        [Required, MaxLength(255)]
        public string DealerName { get; set; }
        [Required, MaxLength(255)]
        public string CustomerName { get; set; }
        [Required, MaxLength(15)]
        public string MobileNumber { get; set; }
        [Required, MaxLength(50)]
        public string GSTNumber { get; set; }
        [Required, MaxLength(10)]
        public string Pincode { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string State { get; set; }
        [Required]
        public int Sales { get; set; }
        public DateTime? Date { get; set; }
        public IFormFile? FileData { get; set; }
        public string? FileBase64 { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

    }
}
