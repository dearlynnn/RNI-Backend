using Microsoft.EntityFrameworkCore;
using UninunezRNIBackend.Data;
using UninunezRNIBackend.Models.Domain;

namespace UninunezRNIBackend.Repositories.AgreementsRequest
{
    public class SQLAgreementRequestRepository : IAgreementsRequestRepository
    {
        private readonly UninunezRNIDbContext dbContext;

        public SQLAgreementRequestRepository(UninunezRNIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<AgreementRequest> CreateAsync(AgreementRequest agreementRequest)
        {
            await dbContext.AgreementRequests.AddAsync(agreementRequest);
            await dbContext.SaveChangesAsync();
            return agreementRequest;
        }

        public async Task<AgreementRequest?> DeleteAsync(Guid id)
        {
            var existingRequestAgreements = await dbContext.AgreementRequests.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRequestAgreements == null)
            {
                return null;
            }
            dbContext.AgreementRequests.Remove(existingRequestAgreements);
            await dbContext.SaveChangesAsync();
            return existingRequestAgreements;
        }

        public async Task<List<AgreementRequest>> GetAllAsync()
        {
            return await dbContext.AgreementRequests.ToListAsync();
        }

        public async Task<AgreementRequest?> GetByIdAsync(Guid id)
        {
            return await dbContext.AgreementRequests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AgreementRequest?> UpdateAsync(Guid id, AgreementRequest agreementRequest)
        {
            var existingRequest = await dbContext.AgreementRequests.FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingRequest == null)
            {
                return null;
            }

            existingRequest.ProposerName = agreementRequest.ProposerName;
            existingRequest.ProposerEmail = agreementRequest.ProposerEmail;
            existingRequest.Role = agreementRequest.Role;
            existingRequest.ProposerPhone = agreementRequest.ProposerPhone;
            existingRequest.Position = agreementRequest.Position;
            existingRequest.Type = agreementRequest.Type;
            existingRequest.ProposedOrganization = agreementRequest.ProposedOrganization;
            existingRequest.RequiresMembershipPayment = agreementRequest.RequiresMembershipPayment;
            existingRequest.MembershipPaymentAmount = agreementRequest.MembershipPaymentAmount;
            existingRequest.RequestDate = agreementRequest.RequestDate;

            await dbContext.SaveChangesAsync();
            return existingRequest;
        }
    }
}
