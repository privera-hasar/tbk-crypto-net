using Jose;
using tbk_crypto.Entities;

namespace tbk_crypto.Mappers
{
    public class PublicKeyMapper : IPublicKeyMapper
    {
        public Jwk map(PublicKey key)
        {
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
