namespace DAL.Models
{
    public class Enrollment
    {
        public int Id { get;  set; } 
        public Student Student { get;  set; }
        public Course Course { get;  set; }
        public DateTime EnrollmentDate { get;  set; }
        public DateTime EnrollmentLastUpdate { get; set; }


        public Enrollment() { }
        public Enrollment(Student student, Course course)

        {
            Student = student;
            Course = course;
            EnrollmentDate = DateTime.Now;
        }
    }
}
