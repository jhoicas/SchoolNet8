using DAL.Context;
using DAL.Models;
using Microsoft.Extensions.Logging;
using School.Contracts.Interfaces;
using School.Contracts.Responses;

namespace School.Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        #region Injected Services

        private readonly ILogger<PaymentService> _logger;
        private readonly SchoolContext schoolContext;

        #endregion Injected Services


        #region "Constructor"
        public PaymentService(SchoolContext schoolContext, ILogger<PaymentService> logger)
        {
            this.schoolContext = schoolContext;
            this._logger = logger;
        }
        #endregion "Constructor"
        public async Task<PaymentResponse> Payment(int idEnrollment)
        {
            var enrollment = await schoolContext.Enrollments.FindAsync(idEnrollment);
            if (enrollment == null)
            {
                return new PaymentResponse { IsSuccessful = false, Message = "enrollment not found" }; 
            }
            enrollment.Course =  await schoolContext.Courses.FindAsync(enrollment.IdCourse);
            enrollment.Student = await schoolContext.Students.FindAsync(enrollment.IdCourse);

            enrollment.IsFeePaid = true;
            schoolContext.Add(new Payment { Amount = enrollment.Course.Fee, PaymentDate = DateTime.UtcNow, Enrollment = enrollment, IdEnrollment = enrollment.Id });
            await schoolContext.SaveChangesAsync();

            return new PaymentResponse { IsSuccessful = true, Message = "payment registred succesfully" };
        }
    }
}
