using UnitTestingApp.Model;

namespace UnitTestingApp.Repository
{
    public interface IPaymentRepository
    {
        Payment? FindById(Guid paymentId);

        List<Payment> FindAll();

        Payment Save(Payment payment);

        Payment EditMessage(Guid paymentId, string message);
    }
}

