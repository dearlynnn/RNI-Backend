using System.Text.Json.Serialization;

namespace UninunezRNIBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProposerRole
    {
        Profesor,
        Director,
        Funcionario,
        Otros
    }
}
