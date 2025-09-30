using Microsoft.EntityFrameworkCore;
using UninunezRNIBackend.Data;
using UninunezRNIBackend.Models.Domain;
using UninunezRNIBackend.Models.DTO;
using UninunezRNIBackend.Models.Enums;

namespace UninunezRNIBackend.Services
{
    public class AgreementRequestService : IAgreementRequestService
    {
        private readonly UninunezRNIDbContext _context;
        private readonly IStatusHistoryService _statusHistoryService;

        public AgreementRequestService(UninunezRNIDbContext context, IStatusHistoryService statusHistoryService)
        {
            _context = context;
            _statusHistoryService = statusHistoryService;
        }

        public async Task<AgreementRequestPublicDto?> GetForRequesterAsync(Guid id, string requesterEmail)
        {
            var request = await _context.AgreementRequests
                .Include(r => r.StatusHistory)
                .FirstOrDefaultAsync(r => r.Id == id && r.ProposerEmail == requesterEmail);

            if (request == null)
                return null;

            var lastStatusUpdate = request.StatusHistory
                .OrderByDescending(h => h.ChangeDate)
                .FirstOrDefault()?.ChangeDate;

            return new AgreementRequestPublicDto
            {
                Id = request.Id,
                ProposerName = request.ProposerName,
                ProposerEmail = request.ProposerEmail,
                Role = request.Role,
                ProposerPhone = request.ProposerPhone,
                Position = request.Position,
                Type = request.Type,
                ProposedOrganization = request.ProposedOrganization,
                RequiresMembershipPayment = request.RequiresMembershipPayment,
                MembershipPaymentAmount = request.MembershipPaymentAmount,
                RequestDate = request.RequestDate,
                Status = request.Status,
                ContactName = request.ContactName,
                ContactPosition = request.ContactPosition,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                LastStatusUpdate = lastStatusUpdate
            };
        }

        public async Task<AgreementRequestRniDto?> GetForRniAsync(Guid id)
        {
            var request = await _context.AgreementRequests
                .Include(r => r.StatusHistory)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return null;

            var lastStatusUpdate = request.StatusHistory
                .OrderByDescending(h => h.ChangeDate)
                .FirstOrDefault()?.ChangeDate;

            return new AgreementRequestRniDto
            {
                Id = request.Id,
                ProposerName = request.ProposerName,
                ProposerEmail = request.ProposerEmail,
                Role = request.Role,
                ProposerPhone = request.ProposerPhone,
                Position = request.Position,
                Type = request.Type,
                ProposedOrganization = request.ProposedOrganization,
                RequiresMembershipPayment = request.RequiresMembershipPayment,
                MembershipPaymentAmount = request.MembershipPaymentAmount,
                RequestDate = request.RequestDate,
                Status = request.Status,
                Observations = request.Observations,
                InternalObservations = request.InternalObservations,
                CommitteeComments = request.CommitteeComments,
                ContactName = request.ContactName,
                ContactPosition = request.ContactPosition,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                AttachedDocumentsPath = request.AttachedDocumentsPath,
                StatusHistory = request.StatusHistory.Select(h => new StatusHistoryDto
                {
                    Status = h.Status,
                    ChangeDate = h.ChangeDate,
                    ChangedBy = h.ChangedBy,
                    Comments = h.Comments
                }).OrderBy(h => h.ChangeDate).ToList(),
                LastStatusUpdate = lastStatusUpdate
            };
        }

        public async Task<bool> IsRequesterAuthorizedAsync(Guid requestId, string requesterEmail)
        {
            return await _context.AgreementRequests
                .AnyAsync(r => r.Id == requestId && r.ProposerEmail == requesterEmail);
        }

        public async Task<Guid> CreateAsync(CreateAgreementRequestDto createDto)
        {
            var agreementRequest = new AgreementRequest
            {
                Id = Guid.NewGuid(),
                ProposerName = createDto.ProposerName,
                ProposerEmail = createDto.ProposerEmail,
                Role = createDto.Role,
                ProposerPhone = createDto.ProposerPhone,
                Position = createDto.Position,
                Type = createDto.Type,
                ProposedOrganization = createDto.ProposedOrganization,
                RequiresMembershipPayment = createDto.RequiresMembershipPayment,
                MembershipPaymentAmount = createDto.MembershipPaymentAmount,
                RequestDate = DateTime.UtcNow,
                Status = RequestStatus.Pending,
                Observations = createDto.Observations,
                ContactName = createDto.ContactName,
                ContactPosition = createDto.ContactPosition,
                ContactEmail = createDto.ContactEmail,
                ContactPhone = createDto.ContactPhone,
                AttachedDocumentsPath = createDto.AttachedDocumentsPath
            };

            _context.AgreementRequests.Add(agreementRequest);
            await _context.SaveChangesAsync();

            // Registrar el estado inicial en el historial
            await _statusHistoryService.RecordStatusChangeAsync(
                agreementRequest.Id, 
                RequestStatus.Pending, 
                "Sistema", 
                "Solicitud creada");

            return agreementRequest.Id;
        }
    }
}