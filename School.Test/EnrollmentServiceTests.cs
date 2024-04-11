using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using School.Contracts.Requests;
using School.Service.Implementations;
using System;

namespace School.Test
{
    [TestClass]
    public class EnrollmentServiceTests
    {
        [TestMethod]
        public async Task RegisterEnrollment_ValidData_ReturnsSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
         
            var schoolContext = new SchoolContext(options);

            // Create sample student and course (assuming they already exist)
            var student = new Student { Id = 1, Name = "Test Student" };
            var course = new Course ("Test Course", 20, DateTime.Now, DateTime.Now.AddDays(30));

            schoolContext.Students.Add(student);
            schoolContext.Courses.Add(course);
            await schoolContext.SaveChangesAsync();

            var mockLogger = new Mock<ILogger<EnrollmentService>>();

            var enrollmentService = new EnrollmentService(schoolContext, mockLogger.Object);

            var enrollmentRequest = new EnrollmentRequest
            {
                StudentId = student.Id,
                CourseId = course.Id
            };

            // Act
            var enrollmentResponse = await enrollmentService.RegisterEnrollment(enrollmentRequest);

            // Assert
            Assert.IsNotNull(enrollmentResponse); // Verify response is not null
            Assert.IsTrue(enrollmentResponse.IsSuccessful); // Verify successful enrollment
            Assert.IsNotNull(enrollmentResponse.Message); // Verify message exists
           
            // Optional: Verify enrollment is added to context
            var enrollments = await schoolContext.Enrollments.ToListAsync();
            Assert.AreEqual(1, enrollments.Count); // Ensure one enrollment added
            var addedEnrollment = enrollments.FirstOrDefault();
            Assert.AreEqual(enrollmentRequest.StudentId, addedEnrollment.IdStudent);
            Assert.AreEqual(enrollmentRequest.CourseId, addedEnrollment.IdCourse);
        }
    }
}
