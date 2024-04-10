namespace School.Contracts.Responses
{
    public class EnrollmentDtoBaseResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime EnrollmentLastUpdate { get; set; }
        public bool IsFeePaid { get; set; }

    }
}
