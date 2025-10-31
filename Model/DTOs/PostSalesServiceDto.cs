namespace HKDataServices.Model.DTOs
{
    public class PostSalesServiceDto
    {
        public Guid PostSalesServiceID { get; set; }
        public Guid CustomerID { get; set; }
        public string? Description { get; set; }
        public IFormFile ImageFile { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
