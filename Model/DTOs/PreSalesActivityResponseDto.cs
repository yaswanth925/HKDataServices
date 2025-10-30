using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class PreSalesActivityResponseDto
    {
        [Required]
        public Guid CustomerID { get; set; }

        public string? ActivityType { get; set; }

        public string? Description { get; set; }

        public string? PoValue { get; set; }

        [Required]
        public IFormFile? FileData { get; set; }
        [Required]
        public IFormFile? PhotoUpload { get; set; }

        public string? CreatedBy { get; set; }

    }
}
