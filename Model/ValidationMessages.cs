using Microsoft.Extensions.Options;

namespace HKDataServices.Model
{
    public class ValidationMessages
    {
        //UpdateTrackingStatus
        public string? AWBNumberEmpty { get; set; }
        public string? AWBNumberMax { get; set; }
        public string? StatusTypeEmpty { get; set; }
        public string? StatusTypeInvalid { get; set; }
        public string? FileNameEmpty { get; set; }
        public string? FileNameMax { get; set; }
        public string? FileDataEmpty { get; set; }
        public string? FileDataMax{ get; set; }
        public string? RemarksMaxLength { get; set; }
        public string? CreatedByEmpty { get; set; }

        //Users
        public string? FirstNameEmpty { get; set; }
        public string? FirstNameMax { get; set; }
        public string? LastNameEmpty { get; set; }
        public string? LastNameMax { get; set; }
        public string?  MobileNumberEmpty { get; set; }
        public string? MobileNumberInvalid { get; set; }
        public string? EmailEmpty { get; set; }
        public string? EmailInvalid { get; set; }
        public string? PasswordEmpty { get; set; }
        public string? PasswordMinLength { get; set; }

        //Customers
        public string? CustomerNameEmpty { get; set; }
        public string? CustomerNameMax { get; set; }
        public string? GSTNumberEmpty { get; set; }
        public string? GSTNumberInvalid { get; set; }
        public string? AddressEmpty { get; set; }
        public string? PincodeEmpty { get; set; }
        public string? CityEmpty { get; set; }
        public string? StateEmpty { get; set; }
        public string? ImageFileEmpty { get; set; }
        public string? ImageFileMax { get; set; }

        //PreSalesActivity
        public string? ActivityTypeEmpty { get; set; }
        public string? DescriptionEmpty { get; set; }
        public string? DescriptionMax { get; set; }
        public string? PoValueEmpty { get; set; }
        public string? PoValueMax { get; set; }
        public string? CreatedByMax { get; set; }

        //PreSalesTarget
        public string EmployeeNameEmpty { get; set; }
        public string EmployeeNameMax { get; set; }
        public DateTime MonthYearEmpty { get; set; }
        public DateTime MonthYearInvalid { get; set; }
        public int TargetYearEmpty { get; set; }
        public int TargetYearInvalid { get; set; }
        public int PreSalesVisitEmpty { get; set; }       
        public int PreSalesActivityEmpty { get; set; }       
        public int PostSalesServiceEmpty { get; set; }
        

        //Accounts
        public string DealerNameEmpty { get; set; }
        public string DealerNameMax { get; set; }
        public int SalesEmpty { get; set; }
        public DateTime? DateEmpty { get; set; }
        public DateTime DateInvalid { get; set; }  
    }
}
