using School.Contracts.Requests;
using School.Contracts.Responses;

namespace School.Contracts.Interfaces
{
    public interface IEnrollmentService
    {
        /// <summary>
        /// Registers a new student enrollment for a course.
        /// </summary>
        /// <param name="enrollment">The enrollment data containing student and course information.</param>
        /// <returns>An EnrollmentResponse object indicating the success or failure of the registration.</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs during enrollment.</exception>

        Task<EnrollmentResponse> RegisterEnrollment(EnrollmentRequest enrollment);

        Task<EnrollmentResponse> RegisterEnrollmentWithPayment(EnrollmentRequest enrollment);

        /// <summary>
        /// Retrieves enrollment information for a specific student identified by their ID.
        /// </summary>
        /// <param name="studentId">The ID of the student whose enrollment details are requested.</param>
        /// <returns>An EnrollmentResponse object containing the enrollment information if found, otherwise null.</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs while retrieving the enrollment.</exception>
        Task<EnrollmentsByStudentDtoResponse> GetEnrollmentByStudentId(int studentId);

        /// <summary>
        /// Retrieves a list of all enrollment records in the system.
        /// </summary>
        /// <returns>A list of EnrollmentResponse objects representing all enrollments.</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs while retrieving all enrollments.</exception>
        Task<IEnumerable<EnrollmentDtoResponse>> GetAllEnrollments();

        /// <summary>
        /// Retrieves enrollment information for a particular course identified by its ID.
        /// </summary>
        /// <param name="courseId">The ID of the course whose enrollments are requested.</param>
        /// <returns>A list of EnrollmentResponse objects containing enrollment details for students enrolled in the course.</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs while retrieving enrollments for the course.</exception>
        Task<EnrollmentsByCourseDtoResponse> GetEnrollmentsByCourseId(int courseId);

        /// <summary>
        /// Updates an existing enrollment record in the system.
        /// </summary>
        /// <param name="enrollmentId">The ID of the enrollment record to be updated.</param>
        /// <param name="enrollment">The updated enrollment data containing student and course information.</param>
        /// <returns>An EnrollmentResponse object indicating the success or failure of the update.</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs while updating the enrollment.</exception>
        Task<EnrollmentResponse> UpdateEnrollment(int enrollmentId, EnrollmentRequest enrollment);

        /// <summary>
        /// Deletes an enrollment record from the system.
        /// </summary>
        /// <param name="enrollmentId">The ID of the enrollment record to be deleted.</param>
        /// <returns>An EnrollmentResponse object indicating the success or failure of the deletion.</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs while deleting the enrollment.</exception>
        Task<EnrollmentResponse> DeleteEnrollment(int enrollmentId);

        /// <summary>
        /// Checks if a student is already enrolled in a specific course.
        /// </summary>
        /// <param name="studentId">The ID of the student to check.</param>
        /// <param name="courseId">The ID of the course to check for enrollment.</param>
        /// <returns>A boolean value indicating whether the student is enrolled (true) or not enrolled (false).</returns>
        /// <exception cref="Exception">Throws an exception if an error occurs while checking enrollment.</exception>
        Task<bool> IsStudentEnrolledInCourse(int studentId, int courseId);

    }
}
