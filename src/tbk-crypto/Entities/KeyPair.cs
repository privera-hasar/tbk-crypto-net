using System.Text.Json.Serialization;

namespace tbk_crypto.Entities
{
    public class KeyPair
    {
        [JsonPropertyName("p")]
        public string? P { get; set; }

        [JsonPropertyName("kty")]
        public string? Kty { get; set; }

        [JsonPropertyName("q")]
        public string? Q { get; set; }

        [JsonPropertyName("d")]
        public string? D { get; set; }

        [JsonPropertyName("e")]
        public string? E { get; set; }

        [JsonPropertyName("use")]
        public string? Use { get; set; }

        [JsonPropertyName("kid")]
        public string? KeyId { get; set; }

        [JsonPropertyName("qi")]
        public string? QI { get; set; }

        [JsonPropertyName("dp")]
        public string? DP { get; set; }

        [JsonPropertyName("alg")]
        public string? Alg { get; set; }

        [JsonPropertyName("dq")]
        public string? DQ { get; set; }

        [JsonPropertyName("n")]
        public string? N { get; set; }
    }
}
