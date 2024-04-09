namespace School.Contracts.Responses
{
    public class CoursesByStudentResponse: StudentDtoResponse
    {
        public List<CourseDtoResponse>? ListCourses { get; set; }
    }
}
