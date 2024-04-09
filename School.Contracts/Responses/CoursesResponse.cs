namespace School.Contracts.Responses
{
    public class CoursesResponse: CourseDtoResponse
    {
        public decimal Amount { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

    }

}
