namespace DAL.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Enrollment? Enrollment { get; set; }
        public int IdEnrollment { get; set; }

    }
}
