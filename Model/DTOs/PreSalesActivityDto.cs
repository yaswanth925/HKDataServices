using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class PreSalesActivityDto
    {
        public Guid PreSalesActivityID { get; set; }
        public Guid CustomerID { get; set; }
        public string? ActivityType { get; set; }
        public string? Description { get; set; }

        [Required]
        public IFormFile FileData { get; set; } = default!;

        public string? PoValue { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; } = default!;

        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
