using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class PreSalesActivityResponseDto
    {
        [Required]
        public Guid ActivityID { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        public string? ActivityType { get; set; }

        public string? Description { get; set; }

        public string? PoValue { get; set; }

        [Required]
        public Byte[]? FileData { get; set; }
        public Byte[]? ImageFile { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

    }
}
