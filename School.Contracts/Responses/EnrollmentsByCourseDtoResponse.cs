namespace School.Contracts.Responses
{
    public class EnrollmentsByCourseDtoResponse: CourseDtoResponse
    {
        public List<StudentDtoResponse>? ListStudents { get; set; }
    }
}
