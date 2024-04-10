namespace DAL.Models
{
    public class Enrollment
    {
        public int Id { get;  set; }
        public int IdStudent { get; set; }

        public Student Student { get;  set; }
        public int IdCourse{ get; set; }

        public Course Course { get;  set; }
        public DateTime EnrollmentDate { get;  set; }
        public DateTime EnrollmentLastUpdate { get; set; }
        public bool IsFeePaid { get; set; }

        public Enrollment() { }
        public Enrollment(int idStudent, int idCourse)

        {
            IdStudent = idStudent;
            IdCourse = idCourse;
            EnrollmentDate = DateTime.Now;
            IsFeePaid = false;
        }
    }
}
