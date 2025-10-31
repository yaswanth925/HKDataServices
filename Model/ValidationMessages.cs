using System.ComponentModel.DataAnnotations;

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
        public string? AddressMax { get; set; }
        public string? PincodeEmpty { get; set; }
        public string? CityEmpty { get; set; }
        public string? StateEmpty { get; set; }
        public string? ImageFileEmpty { get; set; }
        public string? ImageFileMax { get; set; }

        //PreSalesActivityForm

        public Guid? PreSalesActivityID { get; set; }
        [Required]
        public Guid CustomerID { get; set; }
        public string? ActivityType { get; set; }
        public string? ActivityTypeInvalid { get; set; }
        public string? Description { get; set; }
      
        public string? POValue { get; set; }

        //PostSalesService

        public Guid? PostSalesServiceID { get; set; }
        









    }


}
