using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using School.Contracts.Requests;
using School.Contracts.Responses;
using School.Service.Implementations;

namespace School.Test
{
    [TestClass]
    public class CourseServiceTests
    {
     

        [TestMethod]
        public async Task RegisterCourse_NewCourse_ReturnsSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var mockLogger = new Mock<ILogger<CourseService>>();
            var schoolContext = new SchoolContext(options);

            var courseRequest = new CourseRequest
            {
                CourseName = "Test Course",
                Amount = 100,
                DateStart = DateTime.UtcNow.Date,
                DateEnd = DateTime.UtcNow.AddDays(30).Date
            };

            var courseService = new CourseService(schoolContext, mockLogger.Object);

            // Act (using an async method)
            var courseResponse = await courseService.RegisterCourse(courseRequest);

            // Assert
            Assert.IsNotNull(courseResponse); // Check if response is not null
            Assert.Equals("Course registered successfully", courseResponse.Response);

        }



    }
}
