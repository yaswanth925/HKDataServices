namespace HKDataServices.Model.DTOs
{
    public class PreSalesTargetDto
    {
        public Guid TargetID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime MonthYear { get; set; }
        public int TargetYear { get; set; }
        public int PreSalesVisit { get; set; }
        public int PreSalesActivity { get; set; }
        public int PostSalesService { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
