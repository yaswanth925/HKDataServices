namespace HKDataServices.Model
{
        public class Accounts
        {
            public Guid AccountID { get; set; }
            public int DealerCode { get; set; }          
            public string DealerName { get; set; }      
            public string CustomerName { get; set; }     
            public string MobileNumber { get; set; }     
            public string GSTNumber { get; set; }        
            public string Pincode { get; set; }          
            public string City { get; set; }             
            public string State { get; set; }            
            public int Sales { get; set; }               
            public DateTime? Date { get; set; }          
            public byte[]? FileData { get; set; }
            public string? CreatedBy { get; set; }
            public DateTime? Created { get; set; }
            public string? ModifiedBy { get; set; }
            public DateTime? Modified { get; set; }
        }
}



