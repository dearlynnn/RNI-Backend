using Microsoft.EntityFrameworkCore;
using UninunezRNIBackend.Models.Domain;

namespace UninunezRNIBackend.Data
{
    public class UninunezRNIDbContext : DbContext
    {
        public UninunezRNIDbContext(DbContextOptions<UninunezRNIDbContext> options) : base(options)
        {
        }

        public DbSet<AgreementRequest> AgreementRequests { get; set; } = null!;

    public DbSet<AgreementRequestStatusHistory> AgreementRequestStatusHistories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgreementRequest>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.Property(e => e.ProposerName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ProposerEmail).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ProposerPhone).HasMaxLength(20);
                entity.Property(e => e.ProposedOrganization).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Observations).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.InternalObservations).HasMaxLength(1000);
                entity.Property(e => e.CommitteeComments).HasMaxLength(1000);
                entity.Property(e => e.ContactName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ContactPosition).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ContactEmail).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ContactPhone).HasMaxLength(20).IsRequired();
                
                // Configurar la lista de documentos como JSON
                entity.Property(e => e.AttachedDocumentsPath)
                      .HasConversion(
                          v => string.Join(';', v),
                          v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                      );
            });

            modelBuilder.Entity<AgreementRequestStatusHistory>(entity =>
            {
                entity.HasOne(h => h.AgreementRequest)
                      .WithMany(r => r.StatusHistory)
                      .HasForeignKey(h => h.AgreementRequestId);
                      
                entity.Property(h => h.ChangedBy).HasMaxLength(100);
                entity.Property(h => h.Comments).HasMaxLength(500);
            });
        }
    }
}
