using UnitTestingApp.Model;

namespace UnitTestingApp.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Dictionary<Guid, Payment> paymentMap;

        public PaymentRepository()
        {
            paymentMap = new Dictionary<Guid, Payment>();
        }

        public Payment? FindById(Guid paymentId)
        {
            if (paymentId == Guid.Empty)
            {
                throw new ArgumentException("Payment id must not be null");
            }

            return paymentMap.TryGetValue(paymentId, out var payment) ? payment.CopyOf() : null;
        }

        public List<Payment> FindAll()
        {
            return paymentMap.Values.Select(p => p.CopyOf()).ToList();
        }

        public Payment Save(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentException("Payment must not be null");
            }

            if (payment.PaymentId != Guid.Empty && FindById(payment.PaymentId) != null)
            {
                throw new ArgumentException("Payment with id " + payment.PaymentId + " already saved");
            }

            paymentMap[payment.PaymentId] = payment.CopyOf();

            return payment;
        }

        public Payment EditMessage(Guid paymentId, string message)
        {
            if (!paymentMap.ContainsKey(paymentId))
            {
                throw new KeyNotFoundException("Payment with id " + paymentId + " not found");
            }

            Payment payment = paymentMap[paymentId];
            payment.SetMessage(message);
            return payment.CopyOf();
        }
    }

}
