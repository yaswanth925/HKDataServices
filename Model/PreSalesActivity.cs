
using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model
{
    public class PreSalesActivity
    {
        [Key]
        public Guid PreSalesActivityID { get; set; }
        [Required]
        public Guid CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? ActivityType { get; set; }
        public string? Description { get; set; }
        public byte[] FileData { get; set; }
        public string? PoValue { get; set; }
        public byte[] ImageFile { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}