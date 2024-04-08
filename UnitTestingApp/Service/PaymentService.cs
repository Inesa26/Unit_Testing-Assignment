using UnitTestingApp.Model;
using UnitTestingApp.Repository;

namespace UnitTestingApp.Service
{

    public class PaymentService
    {
        private IUserRepository userRepository;
        private IPaymentRepository paymentRepository;
        private IValidationService validationService;

        public PaymentService(IUserRepository userRepository, IPaymentRepository paymentRepository,
                              IValidationService validationService)
        {
            this.userRepository = userRepository;
            this.paymentRepository = paymentRepository;
            this.validationService = validationService;
        }

        public Payment CreatePayment(int userId, double amount)
        {
            validationService.ValidateUserId(userId);
            validationService.ValidateAmount(amount);

            User? user = userRepository.FindById(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User with id " + userId + " not found");
            }
            validationService.ValidateUser(user);

            string paymentMessage = "Payment from user " + user.Name;
            Payment payment = new Payment(userId, amount, paymentMessage);
            return paymentRepository.Save(payment);
        }

        public Payment EditPaymentMessage(Guid paymentId, string newMessage)
        {
            validationService.ValidatePaymentId(paymentId);
            validationService.ValidateMessage(newMessage);

            return paymentRepository.EditMessage(paymentId, newMessage);
        }

        public List<Payment> GetAllByAmountExceeding(double amount)
        {
            return paymentRepository.FindAll()
                .Where(payment => payment.Amount > amount)
                .ToList();
        }
    }
}
