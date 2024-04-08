using UnitTestingApp.Model;

namespace UnitTestingApp.Service
{
    public class BasicValidationService : IValidationService
    {
        public void ValidateAmount(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0");
            }
        }

        public void ValidatePaymentId(Guid paymentId)
        {
            if (paymentId == Guid.Empty)
            {
                throw new ArgumentException("Payment id must not be null");
            }
        }

        public void ValidateUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User id must be greater than 0");
            }
        }

        public void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User must not be null");
            }

            if (user.Status != Status.ACTIVE)
            {
                throw new ArgumentException("User with id " + user.Id + " not in ACTIVE status");
            }
        }

        public void ValidateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Payment message must not be null or empty");
            }
        }
    }
}


