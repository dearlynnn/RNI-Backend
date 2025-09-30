using System.Text.Json.Serialization;
using UninunezRNIBackend.Models.Enums;

namespace UninunezRNIBackend.Models.DTO
{
    /// <summary>
    /// DTO para la vista pública del solicitante - sin información sensible
    /// </summary>
    public class AgreementRequestPublicDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("solicitanteNombre")]
        public string ProposerName { get; set; } = string.Empty;

        [JsonPropertyName("solicitanteEmail")]
        public string ProposerEmail { get; set; } = string.Empty;

        [JsonPropertyName("solicitanteRol")]
        public ProposerRole Role { get; set; }

        [JsonPropertyName("solicitanteTelefono")]
        public string? ProposerPhone { get; set; }

        [JsonPropertyName("cargoSolicitante")]
        public ProposerPosition Position { get; set; }

        [JsonPropertyName("tipoSolicitud")]
        public RequestType Type { get; set; }

        [JsonPropertyName("organizacionPropuesta")]
        public string ProposedOrganization { get; set; } = string.Empty;

        [JsonPropertyName("requierePagoMembresia")]
        public bool RequiresMembershipPayment { get; set; }

        [JsonPropertyName("valorPagoMembresia")]
        public double? MembershipPaymentAmount { get; set; }

        [JsonPropertyName("fechaSolicitud")]
        public DateTime RequestDate { get; set; }

        [JsonPropertyName("estadoSolicitud")]
        public RequestStatus Status { get; set; }

        [JsonPropertyName("contactoNombre")]
        public string ContactName { get; set; } = string.Empty;

        [JsonPropertyName("contactoCargo")]
        public string ContactPosition { get; set; } = string.Empty;

        [JsonPropertyName("contactoEmail")]
        public string ContactEmail { get; set; } = string.Empty;

        [JsonPropertyName("contactoTelefono")]
        public string ContactPhone { get; set; } = string.Empty;

        [JsonPropertyName("fechaUltimaActualizacion")]
        public DateTime? LastStatusUpdate { get; set; }
    }
}