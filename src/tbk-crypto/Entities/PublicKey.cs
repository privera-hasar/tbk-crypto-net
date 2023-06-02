using System.Text.Json.Serialization;

namespace tbk_crypto.Entities
{
    public class PublicKey
    {
        [JsonPropertyName("kty")]
        public string? Kty { get; set; }

        [JsonPropertyName("e")]
        public string? E { get; set; }

        [JsonPropertyName("use")]
        public string? Use { get; set; }

        [JsonPropertyName("kid")]
        public string? Kid { get; set; }

        [JsonPropertyName("alg")]
        public string? Alg { get; set; }

        [JsonPropertyName("n")]
        public string? N { get; set; }
    }
}
