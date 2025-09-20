namespace HKDataServices.Model
{
    public class UpdateTrackingStatus
    {
        public Guid ID { get; set; }
        public string? AWBNumber { get; set; }
        public string? StatusType { get; set; }
        public string? FileName { get; set; }
        public byte[] FileData { get; set; }
        public string? Remarks { get; set; }
        public string? Createdby { get; set; }
        public DateTime? Created { get; set; }
        public string? Modifiedby { get; set; }
        public DateTime? Modified { get; set; }
    }
}
