using Moq;
using UnitTestingApp.Model;
using UnitTestingApp.Repository;
using UnitTestingApp.Service;

namespace UnitTestApp.Tests
{
    public class PaymentServiceTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IPaymentRepository> mockPaymentRepository;
        private readonly Mock<IValidationService> mockValidationService;
        private readonly PaymentService paymentService;

        public PaymentServiceTest()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockPaymentRepository = new Mock<IPaymentRepository>();
            mockValidationService = new Mock<IValidationService>();
            paymentService = new PaymentService(mockUserRepository.Object, mockPaymentRepository.Object, mockValidationService.Object);
        }

        [Fact]
        public void CreatePayment_ValidInput_ReturnsValidPayment()
        {
            // Arrange
            int userId = 1;
            double amount = 100.0;
            var user = new User(userId, "Inessa", Status.ACTIVE);
            mockUserRepository.Setup(repo => repo.FindById(userId)).Returns(user);
            var expectedPayment = new Payment(userId, amount, "Payment from user Inessa");
            mockPaymentRepository.Setup(repo => repo.Save(It.IsAny<Payment>())).Returns(expectedPayment);

            // Act
            var result = paymentService.CreatePayment(userId, amount);

            // Assert
            mockValidationService.Verify(vs => vs.ValidateUserId(userId), Times.Once);
            mockValidationService.Verify(vs => vs.ValidateAmount(amount), Times.Once);
            mockValidationService.Verify(vs => vs.ValidateUser(user), Times.Once);
            Assert.Equal(expectedPayment, result);
        }

        [Fact]
        public void CreatePayment_UserNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            int userId = 1;
            double amount = 100.0;
      
            User user = null;
            mockUserRepository.Setup(repo => repo.FindById(userId)).Returns(user);

            // Assert
            Assert.Throws<InvalidOperationException>(() => paymentService.CreatePayment(userId, amount));
        }
    

        [Fact]
        public void EditPaymentMessage_ValidInput_ReturnsEditedPayment()
        {
            //Arrange
            var paymentId = Guid.NewGuid();
            var newMessage = "New message";
            var expectedPayment = new Payment(1, 100.0, newMessage);
          
            mockPaymentRepository.Setup(repo => repo.EditMessage(paymentId, newMessage)).Returns(expectedPayment);
           
            // Act
            var result = paymentService.EditPaymentMessage(paymentId, newMessage);

            // Assert
            mockValidationService.Verify(vs => vs.ValidatePaymentId(paymentId), Times.Once);
            mockValidationService.Verify(vs => vs.ValidateMessage(newMessage), Times.Once);
            Assert.Equal(expectedPayment, result);
        }

        [Fact]
        public void GetAllByAmountExceeding_ReturnsFilteredPayments()
        {
            //Arrange
            double amount = 50.0;
            var payments = new List<Payment>
            {
                new Payment(1, 100.0, "Payment 1"),
                new Payment(2, 40.0, "Payment 2"),
                new Payment(3, 70.0, "Payment 3")
            };

            mockPaymentRepository.Setup(repo => repo.FindAll()).Returns(payments);

            // Act
            var result = paymentService.GetAllByAmountExceeding(amount);

            // Assert
            Assert.Collection(result,
                payment => Assert.Equal(1, payment.UserId),
                payment => Assert.Equal(3, payment.UserId));
        }
    }
}

