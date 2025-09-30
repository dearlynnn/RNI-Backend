using System.Text.Json.Serialization;

namespace UninunezRNIBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProposerPosition
    {
        Director,
        Coordinador,
        Jefe,
        Decano,
        Investigador,
        Otro
    }
}
