using Jose;
using tbk_crypto.Entities;

namespace tbk_crypto.Mappers
{
    public class KeyPairMapper : IKeyPairMapper
    {
        public Jwk map(KeyPair keys)
        {
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
    }
}
