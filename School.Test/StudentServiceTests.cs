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
        public class StudentServiceTests
        {

        [TestMethod]
        public async Task DeleteStudent_ExistingStudent_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var mockLogger = new Mock<ILogger<StudentService>>(); // Assuming this service depends on CourseService
            var schoolContext = new SchoolContext(options);

            // Create a sample student in the context
            var studentToAdd = new Student("Yoiner", 31);
            schoolContext.Students.Add(studentToAdd);
            await schoolContext.SaveChangesAsync();

            var existingStudentId = studentToAdd.Id;


            var studentService = new StudentService(schoolContext, mockLogger.Object);
            // Act
            var deleteResult = await studentService.DeleteStudent(existingStudentId);

            // Assert
            Assert.IsTrue(deleteResult); // Verify deletion success

            // Optional: Verify student is removed from context
            var deletedStudent = await schoolContext.Students.FindAsync(existingStudentId);
            Assert.IsNull(deletedStudent); // Ensure student is not found after deletion
        }

        [TestMethod]
        public async Task DeleteStudent_NonexistentStudent_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var mockLogger = new Mock<ILogger<StudentService>>();
            var schoolContext = new SchoolContext(options);

            var nonExistentId = 123; // Choose an ID that doesn't exist in the database

            var studentService = new StudentService(schoolContext, mockLogger.Object);


            // Act
            var deleteResult = await studentService.DeleteStudent(nonExistentId);

            // Assert
            Assert.IsFalse(deleteResult); // Verify deletion failure for non-existent student
        }


        [TestMethod]
        public async Task RegisterStudent_ValidData_ReturnsSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
           
            var schoolContext = new SchoolContext(options);

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

            // Assert
            Assert.IsNotNull(studentResponse); // Verify response is not null
            Assert.AreEqual("registred succesfully", studentResponse.Message); // Verify success message

            // Optional: Verify student is added to context
            var students = await schoolContext.Students.ToListAsync();
            Assert.AreEqual(1, students.Count); // Ensure one student added
            var addedStudent = students.FirstOrDefault();
            Assert.AreEqual(studentRequest.FirstName + " " + studentRequest.LastName, addedStudent.Name);
            Assert.AreEqual(studentRequest.Age, addedStudent.Age);
        }

  


    }
}
