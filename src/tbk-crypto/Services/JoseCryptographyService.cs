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

        public string PrivateDecrypt(string token)
        {
            var privateKey = GetKeys();

            var jwe = JWE.Decrypt(token, privateKey);

            return jwe.Plaintext;
        }

        public string PublicEncrypt(string plainText)
        {
            var publicKey = GetPublicKey();
            var jsonPublicKey = _keyRepository.GetJsonPublicKey();

            var recipients = new[] { new JweRecipient(JweAlgorithm.RSA_OAEP_256, publicKey) };

            var extraProtectedHeaders = new Dictionary<string, object> {
                { "app-key", jsonPublicKey }
            };

            return JWE.Encrypt(plainText, recipients, JweEncryption.A256GCM, null, SerializationMode.Compact, JweCompression.DEF, extraProtectedHeaders);
            //return JWT.Encode(plainText, publicKey, JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM);
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
