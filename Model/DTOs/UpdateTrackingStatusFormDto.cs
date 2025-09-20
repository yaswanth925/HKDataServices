using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class UpdateTrackingStatusFormDto
    {
        [Required] public string AWBNumber { get; set; } = default!;
        [Required] public string StatusType { get; set; } = default!;
        public string? FileName { get; set; }
        [Required] public IFormFile FileData { get; set; } = default!;
        public string? Remarks { get; set; }
        public string? Createdby { get; set; }
        public DateTime? Created { get; set; }
        public string Modifiedby { get; set; } = default!;
        public DateTime? Modified { get; set; }
    }
}
