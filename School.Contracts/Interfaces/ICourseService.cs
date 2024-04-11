using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.Contracts.Interfaces
{
    public interface ICourseService
    {
        /// <summary>
        /// Register the Course
        /// </summary>
        /// <param>CourseRequest</param>
        /// <returns>CourseResponse</returns>
        Task<CourseResponse> RegisterCourse(CourseRequest student);

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns>A list of all courses.</returns>
        Task<IEnumerable<CoursesResponse>> GetCourses();

        /// <summary>
        /// Retrieves a course by its ID.
        /// </summary>
        /// <param name="id">The ID of the course to retrieve.</param>
        /// <returns>A CourseResponse object containing the course information, or null if not found.</returns>
        Task<CoursesResponse> GetCourseById(int id);

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        /// <param name="id">The ID of the course to update.</param>
        /// <param name="courseRequest">The updated course data.</param>
        /// <returns>An IActionResult object indicating the update status (success, bad request, not found etc.).</returns>
        Task<CourseResponse> UpdateCourse(int id, CourseRequest courseRequest);

        /// <summary>
        /// Deletes a course by its ID.
        /// </summary>
        /// <param name="id">The ID of the course to delete.</param>
        /// <returns>An IActionResult object indicating the delete status (success, not found etc.).</returns>
        Task<CourseResponse> DeleteCourse(int id);
    }
}
