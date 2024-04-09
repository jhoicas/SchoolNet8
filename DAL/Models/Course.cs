namespace DAL.Models
{
    public class Course
    {
        public int Id { get;  set; } 
        public string Name { get;  set; }
        public decimal Fee { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get; set; }

        public Course(string name, decimal fee, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Fee = fee;
            StartDate = startDate;
            EndDate = endDate;
        }
        public bool IsActive(DateTime date)
        {
            return StartDate <= date && EndDate >= date;
        }
    }
}
