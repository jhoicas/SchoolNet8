using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Contracts.Responses;
using School.Service.Implementations;
using System.Threading.Tasks;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly ILogger<EnrollmentsController> _logger;

        public EnrollmentsController(IEnrollmentService _enrollmentService, ILogger<EnrollmentsController> logger)
        {
            this.enrollmentService = _enrollmentService;
            this._logger = logger;
        }

        [HttpPost("create-enroll")]
        public async Task<IActionResult> CreateEnroll(EnrollmentRequest enrollmenRequest)
        {
            try
            {
                var response = await enrollmentService.RegisterEnrollment(enrollmenRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering enrollment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("create-enroll-with-payment")]
        public async Task<IActionResult> CreateEnrollWithPayment(EnrollmentRequest enrollmenRequest)
        {
            try
            {
                var response = await enrollmentService.RegisterEnrollmentWithPayment(enrollmenRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering enrollment");
                return StatusCode(500, "Internal server error");
            }
        }


        // PUT api/enrollment/{enrollmentId}
        [HttpPut("{enrollmentId}")]
        public async Task<IActionResult> UpdateEnrollment(int enrollmentId, [FromBody] EnrollmentRequest enrollment)
        {
            try
            {
                var response = await enrollmentService.UpdateEnrollment(enrollmentId, enrollment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating enrollment");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/enrollments
        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            try
            {
                var enrollments = await enrollmentService.GetAllEnrollments();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollments");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/enrollment/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetEnrollmentByStudentId(int studentId)
        {
            try
            {
                var enrollment = await enrollmentService.GetEnrollmentByStudentId(studentId);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollment by student ID");
                return StatusCode(404, "Enrollment not found");
            }
        }

        // GET api/enrollment/course/{courseId}
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetEnrollmentsByCourseId(int courseId)
        {
            try
            {
                var enrollments = await enrollmentService.GetEnrollmentsByCourseId(courseId);
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollments by course ID");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/enrollment/exists/student/{studentId}/course/{courseId}
        [HttpGet("exists/student/{studentId}/course/{courseId}")]
        public async Task<IActionResult> IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            try
            {
                var isEnrolled = await enrollmentService.IsStudentEnrolledInCourse(studentId, courseId);
                return Ok(isEnrolled);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking student enrollment");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/enrollment/{enrollmentId}
        [HttpDelete("{enrollmentId}")]
        public async Task<IActionResult> DeleteEnrollment(int enrollmentId)
        {
            try
            {
                var response = await enrollmentService.DeleteEnrollment(enrollmentId);
                if (response.IsSuccessful)
                {
                    return NoContent(); // Successful deletion with no content to return
                }
                return NotFound(response.Message); // Enrollment not found
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting enrollment");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
