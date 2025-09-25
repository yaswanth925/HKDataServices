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
        public string? CreatedbyEmpty { get; set; }



        //Users

        public string FirstNameEmpty { get; set; }
        public string FirstNameMax { get; set; }
        public string LastNameEmpty { get; set; }
        public string LastNameMax { get; set; }
        public string MobileNumberEmpty { get; set; }
        public string MobileNumberInvalid { get; set; }
        public string EmailEmpty { get; set; }
        public string EmailInvalid { get; set; }
        public string PasswordEmpty { get; set; }
        public string PasswordMinLength { get; set; }
       
       
    }


}
