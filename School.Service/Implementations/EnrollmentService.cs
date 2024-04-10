using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.Service.Implementations
{
    public class EnrollmentService : IEnrollmentService
    {
        #region Injected Services

        private readonly ILogger<EnrollmentService> _logger;
        private readonly SchoolContext schoolContext;

        #endregion Injected Services

        #region "Constructor"
        public EnrollmentService(SchoolContext schoolContext, ILogger<EnrollmentService> logger)
        {
            this.schoolContext = schoolContext;
            this._logger = logger;
        }


        #endregion "Constructor"

        #region "Enrollment Actions"


        public async Task<EnrollmentResponse> RegisterEnrollment(EnrollmentRequest enrollment)
        {
            try
            {
                // Create the enrollment referencing the tracked objects
                var newEnrollment = new Enrollment(enrollment.StudentId, enrollment.CourseId);
                var codePayment = schoolContext.Add(newEnrollment);
                await schoolContext.SaveChangesAsync();
                
          
                return new EnrollmentResponse { IsSuccessful = true, Message = $"Successfully registered, payment pending. \nYou can cancel with the code: {codePayment.Entity.Id}." };

            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }
        }

        public async Task<EnrollmentResponse> UpdateEnrollment(int enrollmentId, EnrollmentRequest enrollment)
        {
            try
            {
                var existingEnrollment = await schoolContext.Enrollments
                  .Include(e => e.Student)  // Include Student for efficient retrieval
                  .Include(e => e.Course)   // Include Course for efficient retrieval
                  .FirstOrDefaultAsync(e => e.Id == enrollmentId);

                if (existingEnrollment == null)
                {
                    return new EnrollmentResponse { IsSuccessful = false, Message = "Enrollment not found" };
                }

                // Update student and course references directly
                existingEnrollment.Student = schoolContext.Students.Find(enrollment.StudentId);
                existingEnrollment.Course = schoolContext.Courses.Find(enrollment.CourseId);
                existingEnrollment.EnrollmentLastUpdate = DateTime.UtcNow;
                existingEnrollment.IsFeePaid = enrollment.IsFeePaid;

                schoolContext.Update(existingEnrollment);
                await schoolContext.SaveChangesAsync();

                return new EnrollmentResponse { IsSuccessful = true, Message = "Enrollment updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating enrollment"); // Log the error for debugging
                throw;                                           // Rethrow to allow higher-level handling
            }
        }

        public async Task<List<EnrollmentDtoResponse>> GetAllEnrollments()
        {
            try
            {
                
                var enrollments = await schoolContext.Enrollments
                  .Include(e => e.Student)  
                  .Include(e => e.Course)   
                  .ToListAsync();

                return enrollments.Select(enrollment => new EnrollmentDtoResponse
                {
                    Id = enrollment.Id,
                    StudentId = enrollment.Student.Id,
                    CourseId = enrollment.Course.Id,
                    EnrollmentDate = enrollment.EnrollmentDate,
                    EnrollmentLastUpdate = enrollment.EnrollmentLastUpdate,
                    IsFeePaid = enrollment.IsFeePaid,
                    StudentName = enrollment.Student?.Name,  
                    CourseName = enrollment.Course?.Name   
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollments");
                throw;
            }
        }

        public async Task<EnrollmentsByStudentDtoResponse> GetEnrollmentByStudentId(int studentId)
        {
            try
            {
                var enrolledCourses = await schoolContext.Enrollments
                    .Include(e => e.Course) 
                    .Where(e => e.Student.Id == studentId)
                    .Select(e => e.Course) 
                    .ToListAsync();

                var courses = enrolledCourses.Select(course => new CourseDtoResponse
                {
                    Id = course.Id,
                    CourseName = course.Name,
               
                }).ToList();

                var student = schoolContext.Students.Find(studentId);

                var response = new EnrollmentsByStudentDtoResponse();
                response.StudentId = studentId;
                response.FullName = student?.Name;
                response.ListCourses = courses;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrolled courses for student");
                throw; // Re-throw for appropriate handling (consider specific exception types)
            }
        }

        public async Task<EnrollmentsByCourseDtoResponse> GetEnrollmentsByCourseId(int courseId)
        {
            try
            {
                var enrolledStudents = await schoolContext.Enrollments
                   .Include(e => e.Student)
                   .Include(e => e.Course)
                   .Where(e => e.Course.Id == courseId && e.IsFeePaid)
                   .Select(e => e.Student)
                   .ToListAsync();

                var students = enrolledStudents.Select(student => new StudentDtoResponse
                {
                    StudentId = student.Id,
                    FullName = student.Name,
                    Age = student.Age,

                }).ToList();

                var course = schoolContext.Courses.Find(courseId);

                var response = new EnrollmentsByCourseDtoResponse();
                response.Id = courseId;
                response.CourseName = course?.Name;
                response.ListStudents = students;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollments by course ID");
                throw;
            }
        }

        public async Task<bool> IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            try
            {
                return await schoolContext.Enrollments.AnyAsync(e => e.Student.Id == studentId && e.Course.Id == courseId && e.IsFeePaid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking student enrollment");
                throw;  // Re-throw for appropriate handling
            }
        }

        public async Task<EnrollmentResponse> DeleteEnrollment(int enrollmentId)
        {
            try
            {
                var enrollment = await schoolContext.Enrollments
                  .FirstOrDefaultAsync(e => e.Id == enrollmentId);

                if (enrollment == null)
                {
                    return new EnrollmentResponse { IsSuccessful = false, Message = "Enrollment not found" };
                }

                schoolContext.Remove(enrollment);
                await schoolContext.SaveChangesAsync();

                return new EnrollmentResponse { IsSuccessful = true, Message = "Enrollment deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting enrollment");
                throw; // Re-throw for appropriate handling (optional)
            }
        }

        public async Task<EnrollmentResponse> RegisterEnrollmentWithPayment(EnrollmentRequest enrollment)
        {
            try
            {
                var student = await schoolContext.Students.FindAsync(enrollment.StudentId);
                if (student == null)
                {
                    return null; // Student not found
                }
                var course = await schoolContext.Courses.FindAsync(enrollment.CourseId);

                if (course == null)
                {
                    return null;
                }

                var response = schoolContext.Add(new Enrollment(enrollment.StudentId, enrollment.CourseId));
                await schoolContext.SaveChangesAsync();

                int enrollmentId = response.Entity.Id;


                var enrollmentTemp = await schoolContext.Enrollments.FindAsync(enrollmentId);

                if (enrollmentTemp == null)
                {
                    return null;
                }
                enrollmentTemp.IsFeePaid = true;
                schoolContext.Add(new Payment { Amount = course.Fee, PaymentDate = DateTime.UtcNow, Enrollment = enrollmentTemp });
                await schoolContext.SaveChangesAsync();


                return new EnrollmentResponse { IsSuccessful = true, Message = "registred succesfully" };
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }
        }




        #endregion "Enrollment Actions"
    }
}
