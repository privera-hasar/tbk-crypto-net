using tbk_crypto.Entities;

namespace tbk_crypto.Infrastructure
{
    public interface IKeyRepository
    {
        KeyPair GetHasarKeys();
        PublicKey GetHasarPublicKey();
        string GetJsonHasarPublicKey();
        KeyPair GetTbkKeys();
        PublicKey GetTbkPublicKey();
    }
}