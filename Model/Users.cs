namespace HKDataServices.Model
{
    public class Users
    {
        public Guid ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MobileNumber { get; set; }
        public required string EmailID { get; set; }
        public required string Password { get; set; }
        public required string Createdby { get; set; }
        public DateTime Created { get; set; }
        public required string Modifiedby { get; set; }
        public DateTime Modified { get; set; }
        public bool IsActive { get; set; }
    }
}