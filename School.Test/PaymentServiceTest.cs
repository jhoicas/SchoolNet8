using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using School.Contracts.Requests;
using School.Service.Implementations;

namespace School.Test
{
    [TestClass]
    public class PaymentServiceTest
    {
        [TestMethod]
        public async Task RegisterPayment_ReturnsSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
         
            var schoolContext = new SchoolContext(options);

            //Course
            var mockLoggerCourse = new Mock<ILogger<CourseService>>();
            var courseRequest = new CourseRequest
            {
                CourseName = "Test Course",
                Amount = 100,
                DateStart = DateTime.UtcNow.Date,
                DateEnd = DateTime.UtcNow.AddDays(30).Date
            };

            var courseService = new CourseService(schoolContext, mockLoggerCourse.Object);
            var courseResponse = await courseService.RegisterCourse(courseRequest);

            //Student
            var mockLoggerStudent = new Mock<ILogger<StudentService>>();
            var studentService = new StudentService(schoolContext, mockLoggerStudent.Object);

            var studentRequest = new StudentRequest
            {
                FirstName = "Yoiner",
                LastName = "Castillo",
                Age = 25
            };
            // Act
            var studentResponse = await studentService.RegisterStudent(studentRequest);

            var mockLoggerEnrollment = new Mock<ILogger<EnrollmentService>>();
            var enrollmentService = new EnrollmentService(schoolContext, mockLoggerEnrollment.Object);
            var enrollmentRequest = new EnrollmentRequest
            {
                StudentId = 1,
                CourseId = 1
            };

            // Act
            var enrollmentResponse = await enrollmentService.RegisterEnrollment(enrollmentRequest);


            var mockLoggerPayment = new Mock<ILogger<PaymentService>>();

            var paymentService = new PaymentService(schoolContext, mockLoggerPayment.Object);



            // Act
            var paymentResponse = await paymentService.Payment(1);


            // Assert
            Assert.IsNotNull(courseResponse); // Check if response is not null
            Assert.AreEqual("Course registered successfully", courseResponse.Response);

        }

    }
}
