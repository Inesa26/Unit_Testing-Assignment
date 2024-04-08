using UnitTestingApp.Model;
using UnitTestingApp.Service;

namespace UnitTestingApp.Tests
{
    public class BasicValidationServiceTests
    {
        private BasicValidationService validationService;

        public BasicValidationServiceTests()
        {
            validationService = new BasicValidationService();
        }

        [Fact]
        public void ValidateAmount_ZeroAmount_ArgumentExceptionThrown()
        {
            // Arrange
            double amount = 0.0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => validationService.ValidateAmount(amount));
        }

        [Fact]
        public void ValidateAmount_NegativeAmount_ArgumentExceptionThrown()
        {
            // Arrange
            double amount = -10.0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => validationService.ValidateAmount(amount));
        }

        [Fact]
        public void ValidatePaymentId_EmptyPaymentId_ArgumentExceptionThrown()
        {
            // Arrange
            Guid paymentId = Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => validationService.ValidatePaymentId(paymentId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ValidateUserId_InvalidUserId_ArgumentExceptionThrown(int userId)
        {

            // Act & Assert
            Assert.Throws<ArgumentException>(() => validationService.ValidateUserId(userId));
        }

        [Fact]
        public void ValidateUser_NullUser_ArgumentNullExceptionThrown()
        {
            // Arrange
            User user = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => validationService.ValidateUser(user));
        }

        [Fact]
        public void ValidateUser_InactiveUser_ArgumentExceptionThrown()
        {
            // Arrange
            var user = new User(1, "Inessa", Status.INACTIVE);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => validationService.ValidateUser(user));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateMessage_InvalidMessage_ArgumentExceptionThrown(string message)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => validationService.ValidateMessage(message));
        }
    }
}
