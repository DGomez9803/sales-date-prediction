namespace Domain.Models
{
    public class Customer
    {
        public int CustId { get; set; }
        public string CompanyName { get; set; }
        public string LastOrderDate { get; set; }
        public string NextPredictedOrderDate { get; set; }
    }
}
