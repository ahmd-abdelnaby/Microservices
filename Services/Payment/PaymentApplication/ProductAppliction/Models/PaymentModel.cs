namespace PaymentAppliction
{
    public class PaymentModel
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

    }
}