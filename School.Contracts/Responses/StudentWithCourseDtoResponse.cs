namespace School.Contracts.Responses
{
    public class StudentWithCourseDtoResponse: StudentDtoResponse
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
}
