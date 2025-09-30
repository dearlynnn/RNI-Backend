using UninunezRNIBackend.Data;
using UninunezRNIBackend.Models.Domain;
using UninunezRNIBackend.Models.Enums;

namespace UninunezRNIBackend.Services
{
    public interface IStatusHistoryService
    {
        Task RecordStatusChangeAsync(Guid agreementRequestId, RequestStatus newStatus, string? changedBy = null, string? comments = null);
    }

    public class StatusHistoryService : IStatusHistoryService
    {
        private readonly UninunezRNIDbContext _context;

        public StatusHistoryService(UninunezRNIDbContext context)
        {
            _context = context;
        }

        public async Task RecordStatusChangeAsync(Guid agreementRequestId, RequestStatus newStatus, string? changedBy = null, string? comments = null)
        {
            var historyEntry = new AgreementRequestStatusHistory
            {
                Id = Guid.NewGuid(),
                AgreementRequestId = agreementRequestId,
                Status = newStatus,
                ChangeDate = DateTime.UtcNow,
                ChangedBy = changedBy,
                Comments = comments
            };

            _context.AgreementRequestStatusHistories.Add(historyEntry);
            await _context.SaveChangesAsync();
        }
    }
}