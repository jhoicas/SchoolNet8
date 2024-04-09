namespace DAL.Models
{
    public class Student
    {
        public int Id { get;  set; } 
        public string Name { get;  set; }
        public int Age { get;  set; }

        public Student() { }
        public Student(string name, int age)
        {
            if (age < 18)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Student must be an adult (18 years or older).");
            }

            Name = name;
            Age = age;
        }
      
    }
}
