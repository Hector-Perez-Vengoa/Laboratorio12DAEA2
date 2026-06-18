namespace Laboratorio12DAEA2.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int EnrollmentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public bool isDelete { get; set; } = false;
    }
}
