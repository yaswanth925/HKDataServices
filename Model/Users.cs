namespace HKDataServices.Model
{
    public class Users
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailID { get; set; } = null!;
        public required string MobileNumber { get; set; }
        public required string Password { get; set; }
        public string? OtpCode { get; set; }
        public DateTime? OtpExpiryTime { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public required string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
        public bool IsActive { get; set; } = true;
       


    }
}