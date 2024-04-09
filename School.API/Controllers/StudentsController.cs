using Microsoft.AspNetCore.Mvc;
using School.Contracts.Interfaces;
using School.Contracts.Requests;
using School.Contracts.Responses;
using School.Service.Implementations;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;
        public StudentsController(IStudentService _studentService)
        {
            this.studentService = _studentService;

        }


        [HttpPost("students")]
        public async Task<StudentResponse> CreateStudent(StudentRequest studentRequest)
        {
            return await studentService.RegisterStudent(studentRequest);
        }
       

        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("students/{studentId:int}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            var student = await studentService.GetStudentById(studentId);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPut("students/{studentId:int}")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody] StudentRequest student)
        {
            var response = await studentService.UpdateStudent(studentId, student);
            if (response.Message =="")
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("students/{studentId:int}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var success = await studentService.DeleteStudent(studentId);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("students/{studentId:int}/courses")]
        public async Task<IActionResult> GetEnrolledCourses(int studentId)
        {
            var courses = await studentService.GetEnrolledCourses(studentId);
            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [HttpGet("courses/{courseId:int}/students")]
        public async Task<IActionResult> GetStudentsByCourseId(int courseId)
        {
            var students = await studentService.GetStudentsByCourseId(courseId);
            return Ok(students);
        }

        [HttpGet("exist/students/{studentId:int}/courses/{courseId:int}")]
        public async Task<IActionResult> IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            var isEnrolled = await studentService.IsStudentEnrolledInCourse(studentId, courseId);
            return Ok(new { IsEnrolled = isEnrolled });
        }
    }
}
