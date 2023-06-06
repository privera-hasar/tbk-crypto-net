using Jose;
using tbk_crypto.Infrastructure;

namespace tbk_crypto.Services
{
    /// <summary>
    /// https://github.com/dvsekhvalnov/jose-jwt#rs--and-ps--family
    /// </summary>
    public class JoseCryptographyService : IJoseCryptographyService
    {
        private readonly IKeyRepository _keyRepository;

        public JoseCryptographyService(IKeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }

        public JwsAlgorithm PrivateDecryptJwsAlgorithm { get; set; } = JwsAlgorithm.RS256;
        public JweAlgorithm PrivateDecryptJweAlgorithm { get; set; } = JweAlgorithm.RSA_OAEP;
        public JweEncryption PrivateDecryptJweEncryption { get; set; } = JweEncryption.A256GCM;
        public bool UseJwsAlgorithm { get; set; } = true;

        public JoseCryptographyService Configure(
            JwsAlgorithm privateDecryptJwsAlgorythm = JwsAlgorithm.none,
            JweAlgorithm privateDecryptJweAlgorythm = JweAlgorithm.RSA_OAEP_256,
            JweEncryption privateDecryptJweEncryption = JweEncryption.A256GCM,
            bool useJwsAlgorythm = false)
        {

            PrivateDecryptJwsAlgorithm = privateDecryptJwsAlgorythm;
            PrivateDecryptJweAlgorithm = privateDecryptJweAlgorythm;
            PrivateDecryptJweEncryption = privateDecryptJweEncryption;
            UseJwsAlgorithm = useJwsAlgorythm;
            return this;
        }

        public string PrivateEncrypt(string data)
        {
            var privateKey = GetKeys();

            return JWT.Encode(data, privateKey, JwsAlgorithm.RS256);
        }

        public string PrivateDecrypt(string data)
        {
            var privateKey = GetKeys();

            if (UseJwsAlgorithm) {
                return JWT.Decode(data, privateKey, PrivateDecryptJwsAlgorithm);
            } else {
                return JWT.Decode(data, privateKey, PrivateDecryptJweAlgorithm, PrivateDecryptJweEncryption);
            }
        }

        public string PublicEncrypt(string data)
        {
            var publicKey = GetPublicKey();

            return JWT.Encode(data, publicKey, JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM);
        }

        public string PublicDecrypt(string data)
        {
            var publicKey = GetPublicKey();

            return JWT.Decode(data, publicKey, JwsAlgorithm.RS256);
        }

        public Jwk GetKeys()
        {
            var keys = _keyRepository.GetKeys();

            var privateKey = new Jwk
            {
                E = keys.E,
                N = keys.N,
                P = keys.P,
                Q = keys.Q,
                D = keys.D,
                DP = keys.DP,
                DQ = keys.DQ,
                QI = keys.QI,
                Kty = keys.Kty,
                Use = keys.Use,
                KeyId = keys.KeyId,
                Alg = keys.Alg
            };

            return privateKey;
        }

        public Jwk GetPublicKey()
        {
            var key = _keyRepository.GetPublicKey();

            var publicKey = new Jwk
            {
                Kty = key.Kty,
                E = key.E,
                Use = key.Use,
                KeyId = key.Kid,
                Alg = key.Alg,
                N = key.N
            };

            return publicKey;
        }

    }
}
