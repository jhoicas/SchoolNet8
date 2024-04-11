using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.Contracts.Interfaces
{
    public interface IStudentService
    {
        /// <summary>
        /// Registers a new student in the system.
        /// </summary>
        /// <param name="student">The student information to be registered. (Data in StudentRequest format)</param>
        /// <returns>A StudentResponse object containing the details of the newly registered student or error message if registration fails.</returns>

        Task<StudentResponse> RegisterStudent(StudentRequest student);

        /// <summary>
        /// Retrieves a student by their unique identifier.
        /// </summary>
        /// <param name="studentId">The ID of the student to retrieve.</param>
        /// <returns>A StudentResponse object containing the details of the retrieved student or null if the student is not found.</returns>
        Task<StudentDtoResponse> GetStudentById(int studentId);

        /// <summary>
        /// Retrieves a list of all students in the system.
        /// </summary>
        /// <returns>A List of StudentResponse objects containing details of all students.</returns>
        Task<IEnumerable<StudentDtoResponse>> GetAllStudents();

        /// <summary>
        /// Retrieves a list of students enrolled in a specific course.
        /// </summary>
        /// <param name="courseId">The ID of the course to filter students by.</param>
        /// <returns>A List of StudentResponse objects containing details of students enrolled in the specified course.</returns>
        Task<IEnumerable<StudentDtoResponse>> GetStudentsByCourseId(int courseId);

        /// <summary>
        /// Updates the information of an existing student.
        /// </summary>
        /// <param name="studentId">The ID of the student to update.</param>
        /// <param name="student">The updated student information. (Data in StudentRequest format)</param>
        /// <returns>A StudentResponse object containing the details of the updated student or error message if update fails.</returns>
        Task<StudentResponse> UpdateStudent(int studentId, StudentRequest student);
        /// <summary>
        /// Deletes a student from the system.
        /// </summary>
        /// <param name="studentId">The ID of the student to delete.</param>
        /// <returns>A boolean value indicating success (true) or failure (false) of the deletion operation.</returns>
        Task<bool> DeleteStudent(int studentId);

        /// <summary>
        /// Checks if a student is enrolled in a specific course.
        /// </summary>
        /// <param name="studentId">The ID of the student to check.</param>
        /// <param name="courseId">The ID of the course to check enrollment in.</param>
        /// <returns>A boolean value indicating whether the student is enrolled (true) or not enrolled (false) in the specified course.</returns>
        Task<bool> IsStudentEnrolledInCourse(int studentId, int courseId);
        /// <summary>
        /// Retrieves a list of courses a student is enrolled in.
        /// </summary>
        /// <param name="studentId">The ID of the student to retrieve enrolled courses for.</param>
        /// <returns>A List of CourseResponse objects containing details of the courses the student is enrolled in.</returns>
        Task<CoursesByStudentResponse> GetEnrolledCourses(int studentId);

    }
}
