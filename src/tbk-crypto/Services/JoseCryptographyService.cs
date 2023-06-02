using Jose;
using tbk_crypto.Infrastructure;

namespace tbk_crypto.Services
{
    /// <summary>
    /// https://github.com/dvsekhvalnov/jose-jwt#rs--and-ps--family
    /// </summary>
    internal class JoseCryptographyService
    {
        private readonly IKeyRepository _keyRepository;

        public JoseCryptographyService(IKeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }
        public string JosePrivateEncrypt(string data)
        {
            var privateKey = GetKeys();

            return JWT.Encode(data, privateKey, JwsAlgorithm.RS256);
            //return JWT.Encode(data, privateKey, JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM);
        }

        public string JosePrivateDecrypt(string data)
        {
            var privateKey = GetKeys();

            return JWT.Decode(data, privateKey, JwsAlgorithm.RS256);
        }

        public string JosePublicEncrypt(string data)
        {
            var publicKey = GetPublicKey();

            return JWT.Encode(data, publicKey, JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM);
        }

        public string JosePublicDecrypt(string data)
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
