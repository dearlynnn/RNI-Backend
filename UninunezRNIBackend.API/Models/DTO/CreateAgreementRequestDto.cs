using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UninunezRNIBackend.Models.Enums;

namespace UninunezRNIBackend.Models.DTO
{
    public class CreateAgreementRequestDto
    {
        [Required]
        [JsonPropertyName("solicitanteNombre")]
        public string ProposerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [JsonPropertyName("solicitanteEmail")]
        public string ProposerEmail { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("solicitanteRol")]
        public ProposerRole Role { get; set; }

        [JsonPropertyName("solicitanteTelefono")]
        public string? ProposerPhone { get; set; }

        [Required]
        [JsonPropertyName("cargoSolicitante")]
        public ProposerPosition Position { get; set; }

        [Required]
        [JsonPropertyName("tipoSolicitud")]
        public RequestType Type { get; set; }

        [Required]
        [JsonPropertyName("organizacionPropuesta")]
        public string ProposedOrganization { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("requierePagoMembresia")]
        public bool RequiresMembershipPayment { get; set; }

        [JsonPropertyName("valorPagoMembresia")]
        public double? MembershipPaymentAmount { get; set; }

        [Required]
        [JsonPropertyName("observaciones")]
        public string Observations { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("contactoNombre")]
        public string ContactName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("contactoCargo")]
        public string ContactPosition { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [JsonPropertyName("contactoEmail")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("contactoTelefono")]
        public string ContactPhone { get; set; } = string.Empty;

        [JsonPropertyName("documentosAdjuntosRuta")]
        public List<string> AttachedDocumentsPath { get; set; } = new List<string>();
    }
}