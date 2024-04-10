using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.Service.Implementations
{
    public class StudentService: IStudentService
    {
        #region Injected Services

        private readonly ILogger<StudentService> _logger;
        private readonly SchoolContext schoolContext;
       

        #endregion Injected Services

        #region "Constructor"
        public StudentService(SchoolContext schoolContext, ILogger<StudentService> logger)
        {
            this.schoolContext = schoolContext;
            this._logger = logger;
        }

        #endregion "Constructor"

        #region "Student Actions"
        public async Task<bool> DeleteStudent(int studentId)
        {
            try
            {
                var student = await schoolContext.Students.FindAsync(studentId);
                if (student == null)
                {
                    return false; // Student not found
                }

                schoolContext.Remove(student);
                await schoolContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student");
                throw; // Re-throw for appropriate handling
            }
        }

        public async Task<List<StudentDtoResponse>> GetAllStudents()
        {
            try
            {
                var students = await schoolContext.Students.ToListAsync();
                return students.Select(student => new StudentDtoResponse
                {
                    StudentId = student.Id,
                    FullName = student.Name,
                    Age = student.Age,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all students");
                throw;
            }
        }

        public async Task<CoursesByStudentResponse?> GetEnrolledCourses(int studentId)
        {
            try
            {
                var courses = await schoolContext.Enrollments
            .Where(e => e.Student.Id == studentId)
            .Select(e => e.Course)
            .ToListAsync();

             
                if (courses == null)
                {
                    return null; // courses not found
                }

                var student = await schoolContext.Students.FindAsync(studentId);

                if (student == null)
                {
                    return null; // Student not found
                }


                var coursesByStudent = new CoursesByStudentResponse();
                coursesByStudent.StudentId = studentId;
                coursesByStudent.FullName = student.Name;
                coursesByStudent.Age = student.Age;
                coursesByStudent.ListCourses = courses.Select(course => new CourseDtoResponse { Id = course.Id, CourseName = course.Name, }).ToList();

                
                return coursesByStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrolled courses for student");
                throw;
            }
        }

        public async Task<StudentDtoResponse?> GetStudentById(int studentId)
        {
            try
            {
                var student = await schoolContext.Students.FindAsync(studentId);
                if (student == null)
                {
                    return null; // Student not found
                }

                return new StudentDtoResponse
                {
                    StudentId = student.Id,
                    FullName = student.Name,
                    Age = student.Age,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student by ID");
                throw;
            }
        }

        public async Task<List<StudentDtoResponse>> GetStudentsByCourseId(int courseId)
        {
            try
            {
                var students = await schoolContext.Enrollments
       .Where(e => e.Course.Id == courseId)
       .Select(e => e.Student)
       .ToListAsync();


                return students.Select(student => new StudentDtoResponse
                {
                    StudentId = student.Id,
                    FullName = student.Name,
                    Age = student.Age,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving students by course ID");
                throw;
            }
        }

        public async Task<bool> IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            try
            {
                return await schoolContext.Enrollments.AnyAsync(s => s.Student.Id == studentId && s.Course.Id == courseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking student enrollment");
                throw;
            }
        }
       

        public async Task<StudentResponse> RegisterStudent(StudentRequest student)
        {
            try
            {
                schoolContext.Add(new Student(student.FirstName + ' '+student.LastName, student.Age ));
                await schoolContext.SaveChangesAsync();
                return new StudentResponse { Message = "registred succesfully" };
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }
        }

        public async Task<StudentResponse> UpdateStudent(int studentId, StudentRequest student)
        {
            try
            {
                var existingStudent = await schoolContext.Students.FindAsync(studentId);
                if (existingStudent == null)
                {
                    return new StudentResponse { Message = "Student not found" }; // Informative error message
                }

                // Update properties based on request data
                existingStudent.Name = student.FirstName + " "+ student.LastName;
                existingStudent.Age = student.Age ;


                schoolContext.Update(existingStudent);
                await schoolContext.SaveChangesAsync();

                return new StudentResponse { Message = "Student updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating student");
                throw; // Re-throw for appropriate handling
            }
        }

      

   

        #endregion "Student Actions"
    }
}
