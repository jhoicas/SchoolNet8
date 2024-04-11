using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.Service.Implementations
{
    public class CourseService : ICourseService
    {
        #region Injected Services

        private readonly ILogger<CourseService> _logger;
        private readonly SchoolContext schoolContext;

        #endregion Injected Services

        #region "Constructor"
        public CourseService(SchoolContext schoolContext, ILogger<CourseService> logger)
        {
            this.schoolContext = schoolContext;
            this._logger = logger;
        }
        #endregion "Constructor"

        #region "Course Actions"
        public async Task<CourseResponse> RegisterCourse(CourseRequest course)
        {
            try
            {
                // Check if course already exists
                var existingCourse = await schoolContext.Courses
                  .FirstOrDefaultAsync(c => c.Name == course.CourseName);  // Assuming course name is the unique identifier

                if (existingCourse != null)
                {
                    return new CourseResponse { Response = "Course already exists" };
                }

                // Course doesn't exist, proceed with registration
                schoolContext.Add(new Course(course.CourseName, course.Amount, course.DateStart, course.DateEnd));
                await schoolContext.SaveChangesAsync();
                return new CourseResponse { Response = "Course registered successfully" };
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType().ToString();
                _logger.LogError(exceptionType, ex);
                throw;
            }

        }

        public async Task<IEnumerable<CoursesResponse>> GetCourses()
        {
            var courses = await schoolContext.Courses.ToListAsync();

            // Map Course objects to CoursesResponse objects
            var coursesResponse = courses.Select(course => new CoursesResponse
            {
                Id = course.Id,
                CourseName = course.Name,
                Amount = course.Fee,
                DateStart = course.StartDate.Date,
                DateEnd = course.EndDate.Date
            }).ToList();

            return coursesResponse;
        }

        public async Task<CoursesResponse?> GetCourseById(int id)
        {
            try
            {
                var course = await FindCourseByIdAsync(id);

                if (course == null)
                {
                    // Consider returning a 404 Not Found response in this case
                    return null;
                }

                return new CoursesResponse
                {
                    Id = course.Id,
                    CourseName = course.Name,
                    Amount = course.Fee,
                    DateStart = course.StartDate,
                    DateEnd = course.EndDate
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course with ID {Id}", id);
                throw;
            }
        }

        public async Task<CourseResponse?> UpdateCourse(int id, CourseRequest courseRequest)
        {
            try
            {
                var course = await FindCourseByIdAsync(id);
                if (course == null)
                {
                    // Consider returning a 404 Not Found response in this case
                    return new CourseResponse { Response = "Course not found" };
                }

                // Apply updates to the course object
                course.Name = courseRequest.CourseName;
                course.Fee = courseRequest.Amount;
                course.StartDate = courseRequest.DateStart;
                course.EndDate = courseRequest.DateEnd;

                schoolContext.Update(course);
                await schoolContext.SaveChangesAsync();

                return new CourseResponse { Response = "Course updated successfully" };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., optimistic locking)
                _logger.LogError(ex, "Concurrency error updating course with ID {Id}", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course with ID {Id}", id);
                throw;
            }
        }

        public async Task<CourseResponse?> DeleteCourse(int id)
        {
            try
            {
                var course = await FindCourseByIdAsync(id);
                if (course == null)
                {
                    // Consider returning a 404 Not Found response in this case
                    return new CourseResponse { Response = "Course not found" };
                }

                schoolContext.Remove(course);
                await schoolContext.SaveChangesAsync();

                return new CourseResponse { Response = "Course deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course with ID {Id}", id);
                throw;
            }
        }
        private async Task<Course?> FindCourseByIdAsync(int id)
        {
            return await schoolContext.Courses.FindAsync(id);
        }



        #endregion "Course Actions"
    }
}
