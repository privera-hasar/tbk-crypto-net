using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    internal interface IKeyRepository
    {
        KeyPair GetKeys();
        PublicKey GetPublicKey();
    }
}