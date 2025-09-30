using System.Text.Json.Serialization;

namespace UninunezRNIBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestType
    {
        FirmaInterinstitucional,
        FirmaMovilidad,
        FirmaInvestigacion,
        AdhesionRed,
        AdhesionAsociacion
    }
}
