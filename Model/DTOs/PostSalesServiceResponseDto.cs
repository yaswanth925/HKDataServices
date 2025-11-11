using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class PostSalesServiceResponseDto
    {
        [Required]
        public Guid ServiceID { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        public string? Description { get; set; }
        public Byte[]? ImageFile { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
