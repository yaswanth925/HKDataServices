using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class UpdateTrackingStatusResponseDto
    {
        public Guid ID { get; set; }
        public string? AWBNumber { get; set; }
        public string? StatusType { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        [Required]
        public string ModifiedBy { get; set; } = default!;
        public DateTime? Modified { get; set; }

    }
}