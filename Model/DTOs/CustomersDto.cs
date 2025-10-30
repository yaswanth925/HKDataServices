namespace HKDataServices.Model.DTOs
{
    public class CustomersDto
    {
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? GSTNumber { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Description { get; set; }
        public IFormFile? PhotoUpload { get; set; }
        public string? CreatedBy { get; set; }
        public string? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Modified { get; set; }
    }
}
