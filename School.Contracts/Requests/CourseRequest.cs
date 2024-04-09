
using System.ComponentModel.DataAnnotations;


namespace School.Contracts.Requests
{
    /// <summary>
    /// Represents a course request model.
    /// </summary>
    public class CourseRequest
    {

        /// <summary>
        /// The Name of the course.
        /// </summary>
        [Required]
        public string CourseName { get; set; }
        /// <summary>
        /// The course fee.
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
        /// <summary>
        /// The start date of the course. (format: YYYY-MM-DD)
        /// </summary>
        [Required]
        public DateTime DateStart { get; set; }
        /// <summary>
        /// The end date of the course. (format: YYYY-MM-DD)
        /// </summary>
        [Required]
        public DateTime DateEnd { get; set; }

    }
}
