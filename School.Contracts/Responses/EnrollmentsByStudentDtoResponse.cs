namespace School.Contracts.Responses
{
    public class EnrollmentsByStudentDtoResponse: StudentDtoResponse
    {
        public List<CourseDtoResponse>? ListCourses { get; set; }
    }
}
