using UninunezRNIBackend.Models.Domain;
using UninunezRNIBackend.Models.DTO;

namespace UninunezRNIBackend.Services
{
    public interface IAgreementRequestService
    {
        Task<AgreementRequestPublicDto?> GetForRequesterAsync(Guid id, string requesterEmail);
        Task<AgreementRequestRniDto?> GetForRniAsync(Guid id);
        Task<bool> IsRequesterAuthorizedAsync(Guid requestId, string requesterEmail);
        Task<Guid> CreateAsync(CreateAgreementRequestDto createDto);
    }
}