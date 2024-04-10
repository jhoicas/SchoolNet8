using School.Contracts.Responses;

namespace School.Contracts.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> Payment(int idEnrollment);
    }
}
