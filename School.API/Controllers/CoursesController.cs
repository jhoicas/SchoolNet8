using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseService _courseService, ILogger<CoursesController> logger)
        {
            this.courseService = _courseService;
            this._logger = logger;

        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <param courseRequest="CourseRequest">The course request data.</param>
        /// <returns>The course creation response.</returns>
        /// <response code="201">Course created successfully.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        public async Task<CourseResponse> CreateCourse(CourseRequest courseRequest)
        {
            return  await courseService.RegisterCourse(courseRequest);
        }

        [HttpGet]
        public async Task<List<CoursesResponse>> GetCourses()
        {
            return await courseService.GetCourses();
        }

        /// <summary>
        /// Retrieves a course by its ID.
        /// </summary>
        /// <param name="id">The ID of the course to retrieve.</param>
        /// <returns>The course information if found, otherwise a 404 Not Found response.</returns>
        /// <response code="200">Course found successfully.</response>
        /// <response code="404">Course not found.</response>
        [HttpGet("{id}")]
        public async Task<CoursesResponse> GetCourseById(int id)
        {
            try
            {
                return await courseService.GetCourseById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course with ID {Id}", id);
                throw; // Re-throw to let the controller handle the exception
            }
        }

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        /// <param name="id">The ID of the course to update.</param>
        /// <param name="courseRequest">The updated course data.</param>
        /// <returns>An object indicating whether the update was successful.</returns>
        /// <response code="200">Course updated successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Course not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseRequest courseRequest)
        {
            try
            {
                await courseService.UpdateCourse(id, courseRequest);
                return NoContent(); // 204 No Content response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course with ID {Id}", id);
                return StatusCode(500, "Error updating course"); // Or handle specific exceptions differently
            }
        }

        /// <summary>
        /// Deletes a course by its ID.
        /// </summary>
        /// <param name="id">The ID of the course to delete.</param>
        /// <returns>No content if the course is deleted successfully.</returns>
        /// <response code="204">Course deleted successfully.</response>
        /// <response code="404">Course not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await courseService.DeleteCourse(id);
                return NoContent(); // 204 No Content response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course with ID {Id}", id);
                return StatusCode(500, "Error deleting course"); // Or handle specific exceptions differently
            }
        }

    }
}
