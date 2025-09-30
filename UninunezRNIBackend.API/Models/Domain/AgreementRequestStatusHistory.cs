using System;
using System.ComponentModel.DataAnnotations;
using UninunezRNIBackend.Models.Enums;

namespace UninunezRNIBackend.Models.Domain
{
    public class AgreementRequestStatusHistory
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AgreementRequestId { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? ChangedBy { get; set; } // email o usuario
        public string? Comments { get; set; }

        public AgreementRequest? AgreementRequest { get; set; }
    }
}
