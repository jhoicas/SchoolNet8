using DAL.Context;
using DAL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using School.Service.Implementations;
using Xunit;

namespace School.Test
{
    [TestClass]
    public class PaymentServiceTest
    {
        private readonly SchoolContext schoolContext;
        private readonly ILogger<PaymentService> _logger;

        public PaymentServiceTest()
        {
            schoolContext = new Mock<SchoolContext>();
            _logger = new Mock<ILogger<PaymentService>>();
        }

        [Fact]
        public async Task Payment_ReturnsSuccessForValidEnrollment()
        {
            // Arrange
            var enrollmentId = 1;
            var expectedEnrollment = new Enrollment { Id = enrollmentId };

            schoolContext.Setup(context => context.FindAsync<Enrollment>(enrollmentId))
              .ReturnsAsync(expectedEnrollment);

            var paymentService = new PaymentService(schoolContext.Object, _logger.Object);

            // Act
            var result = await paymentService.Payment(enrollmentId);

            // Assert
            Assert.True(result.IsCompletedSuccessfully);

            // Verify service interactions (consider specific logic you want to test)
            _schoolContextMock.Verify(context => context.FindAsync<Enrollment>(enrollmentId), Times.Once);
            // Additional assertions based on your PaymentService implementation
        }

    }
}
