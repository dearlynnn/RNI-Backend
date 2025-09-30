using UninunezRNIBackend.Models.Domain;

namespace UninunezRNIBackend.Repositories.AgreementsRequest
{
    public interface IAgreementsRequestRepository
    {
        Task<List<AgreementRequest>> GetAllAsync();

        Task<AgreementRequest?> GetByIdAsync(Guid id);

        Task<AgreementRequest> CreateAsync(AgreementRequest agreementRequest);

        Task<AgreementRequest?> UpdateAsync(Guid id, AgreementRequest agreementRequest);

        Task<AgreementRequest?> DeleteAsync(Guid id);
    }
}
