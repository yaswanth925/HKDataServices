using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class PostSalesServiceResponseDto
    {
        [Required]
        public Guid PostSalesServiceID { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        public string? Description { get; set; }
        public string? ImageFileBase64 { get; set; }
        public string? CreatedBy { get; set; }
        public string? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Modified { get; set; }
    }
}
