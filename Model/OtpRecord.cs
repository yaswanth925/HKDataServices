namespace HKDataServices.Model
{
    public class OtpRecord
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }
}
