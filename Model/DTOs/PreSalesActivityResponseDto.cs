using System.ComponentModel.DataAnnotations;

namespace HKDataServices.Model.DTOs
{
    public class PreSalesActivityResponseDto
    {
        [Required]
        public Guid PreSalesActivityID { get; set; }

        [Required]
        public Guid CustomerID { get; set; }

        public string? ActivityType { get; set; }

        public string? Description { get; set; }

        public string? PoValue { get; set; }

        [Required]
        public string? FileDataBase64 { get; set; }
        public string? ImageFileBase64 { get; set; }
        public string? CreatedBy { get; set; }
        public string? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Modified { get; set; }

    }
}
