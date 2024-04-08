namespace UnitTestingApp.Model
{
    public class Payment
    {
        public Guid PaymentId { get; }
        public int UserId { get; }
        public double Amount { get; }
        private string message;

        public Payment(int userId, double amount, string message)
            : this(Guid.NewGuid(), userId, amount, message)
        {
        }

        private Payment(Guid paymentId, int userId, double amount, string message)
        {
            PaymentId = paymentId;
            UserId = userId;
            Amount = amount;
            this.message = message;
        }

        public Payment CopyOf() => new Payment(PaymentId, UserId, Amount, message);

        public string GetMessage() => message;

        public void SetMessage(string message) => this.message = message;
    }
}
