using Jose;
using tbk_crypto.Entities;

namespace tbk_crypto.Mappers
{
    public interface IPublicKeyMapper
    {
        Jwk map(PublicKey key);
    }
}