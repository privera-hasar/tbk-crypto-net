using Jose;
using tbk_crypto.Entities;

namespace tbk_crypto.Mappers
{
    public interface IKeyPairMapper
    {
        Jwk map(KeyPair keys);
    }
}