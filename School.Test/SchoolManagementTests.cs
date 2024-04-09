using DAL.Models;
using Xunit;
using Assert = Xunit.Assert;


namespace School.Test
{

    public class SchoolManagementTests
    {

        //[Fact]
        //public void RegisterCourse_AddsCourseToList()
        //{
        //    var schoolManagement = new SchoolManagement();
        //    var course = new Course(
        //        "Programación 101",
        //         100,
        //      new DateTime(2024, 5, 1),
        //        new DateTime(2024, 6, 30));

        //    schoolManagement.RegisterCourse(course);

           
        //    Assert.Single(schoolManagement.Courses);
  
        //    int courseCount = schoolManagement.Courses.Count();

        //    Assert.Equal(1, courseCount);

        //}

        //[Fact]
        //public void RegisterStudent_AddsAdultStudentToList()
        //{
        //    var schoolManagement = new SchoolManagement();
        //    var student = new Student(
        //         "Juan Pérez",
        //        25);

        //    schoolManagement.RegisterStudent(student);

        //    Assert.Single(schoolManagement.Students);

        //    int studentsCount = schoolManagement.Students.Count();

        //    Assert.Equal(1, studentsCount);
        //}

        //[Fact]
        //public void RegisterStudent_DoesNotAddMinorStudentToList()
        //{
        //    var schoolManagement = new SchoolManagement();
        //    var student = new Student(

        //      "Ana López",
        //      17
        //    );

        //    schoolManagement.RegisterStudent(student);

        //    Assert.Empty(schoolManagement.Students);
        //}

        //[Fact]
        //public void Enroll_EnrollsStudentInActiveCourse()
        //{
        //    var schoolManagement = new SchoolManagement();

        //    var student = new Student
        //   ( "Juan Pérez",
        //     25
        //    );
        //    schoolManagement.RegisterStudent(student);

        //    var course = new Course
        //    (
        //         "Programación 101",
        //        100,
        //         DateTime.Today,
        //        DateTime.Today.AddMonths(1)
        //    );
        //    schoolManagement.RegisterCourse(course);

        //    //student.Enroll(course);

        //    //Assert.Single(student.EnrolledCourses);
        //    //Assert.Equal(course, student.EnrolledCourses[0]);
        //}

        //[Fact]
        //public void GetEnrolledStudents_ReturnsStudentsInDateRange()
        //{
        //    //var schoolManagement = new SchoolManagement();

        //    //var student1 = new Student
        //    //{
        //    //    Name = "Juan Pérez",
        //    //    Age = 25
        //    //};
        //    //schoolManagement.RegisterStudent(student1);

        //    //var student2 = new Student
        //    //{
        //    //    Name = "Ana López",
        //    //    Age = 23
        //    //};
        //    //schoolManagement.RegisterStudent(student2);

        //    //var course1 = new Course
        //    //{
        //    //    Name = "Programación 101",
        //    //    Fee = 100,
        //    //    StartDate = new DateTime(2024, 5, 1),
        //    //    EndDate = new DateTime(2024, 6, 30)
        //    //};
        //    //schoolManagement.RegisterCourse(course1);

        //    //var course2 = new Course
        //    //{
        //    //    Name = "Bases de Datos",
        //    //    Fee = 150,
        //    //    StartDate = new DateTime(2024, 7, 1),
        //    //    EndDate = new DateTime(2024, 8, 30)
        //    //};
        //    //schoolManagement.RegisterCourse(course2);

        //    //student1.Enroll(course1);
        //    //student2.Enroll(course1);
        //    //student2.Enroll(course2);

        //    //var startDate = new DateTime(2024, 5, 15);
        //    //var endDate = new DateTime(2024, 7, 15);

        //    //var enrolledStudents = schoolManagement.GetEnrolledStudents(startDate, endDate);

        //    //Assert.Equal(2, enrolledStudents.Count);
        //    //Assert.Contains(student1, enrolledStudents);
        //    //Assert.Contains(student2, enrolledStudents);
        //}

    }
}
