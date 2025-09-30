using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UninunezRNIBackend.Models.Enums;

namespace UninunezRNIBackend.Models.Domain
{
    public class AgreementRequest
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("proposerName")]
        public string ProposerName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("proposerEmail")]
        public string ProposerEmail { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("proposerRole")]
        public ProposerRole Role { get; set; } = ProposerRole.Director;

        [JsonPropertyName("proposerPhone")]
        public string? ProposerPhone { get; set; }

        [Required]
        [JsonPropertyName("proposerPosition")]
        public ProposerPosition Position { get; set; } = ProposerPosition.Director;

        [Required]
        [JsonPropertyName("requestType")]
        public  RequestType Type { get; set; } = RequestType.FirmaInvestigacion;

        [Required]
        [JsonPropertyName("organizacionPropuesta")]
        public string ProposedOrganization { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("requierePagoMembresia")]
        public bool RequiresMembershipPayment { get; set; } = false;

        [JsonPropertyName("valorPagoMembresia")]
        public double? MembershipPaymentAmount { get; set; }

        [JsonPropertyName("fechaSolicitud")]
        public DateTime RequestDate { get; set; } = DateTime.Today;

        [Required]
        [JsonPropertyName("estadoSolicitud")]
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        [Required]
        [JsonPropertyName("observaciones")]
        public string Observations { get; set; } = string.Empty;

        [JsonPropertyName("observacionesInternas")]
        public string? InternalObservations { get; set; }

        [JsonPropertyName("comentariosComite")]
        public string? CommitteeComments { get; set; }

        [Required]
        [JsonPropertyName("contactoNombre")]
        public string ContactName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("contactoCargo")]
        public string ContactPosition { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("contactoEmail")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("contactoTelefono")]
        public string ContactPhone { get; set; } = string.Empty;

        [JsonPropertyName("documentosAdjuntosRuta")]
        public List<string> AttachedDocumentsPath { get; set; } = new List<string>();

        // Propiedades de navegación
        public virtual ICollection<AgreementRequestStatusHistory> StatusHistory { get; set; } = new List<AgreementRequestStatusHistory>();

    }
}
