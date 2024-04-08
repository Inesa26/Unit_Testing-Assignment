using UnitTestingApp.Model;

namespace UnitTestingApp.Service
{
    public interface IValidationService
    {
        void ValidateAmount(double amount);

        void ValidatePaymentId(Guid paymentId);

        void ValidateUserId(int userId);

        void ValidateUser(User user);

        void ValidateMessage(string message);
    }
}
