namespace HKDataServices.Model
{
    public class PostSalesService
    {
        public Guid PostSalesServiceID { get; set; }
        public Guid CustomerID { get; set; }
        public string? Description { get; set; }
        public byte[] ImageFile { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
