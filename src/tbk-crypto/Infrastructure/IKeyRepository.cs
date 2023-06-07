using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    public interface IKeyRepository
    {
        KeyPair GetKeys();
        PublicKey GetPublicKey();
        string GetJsonPublicKey();
    }
}