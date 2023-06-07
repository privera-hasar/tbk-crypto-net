using Jose;

namespace tbk_crypto.Services
{
    public interface IJoseCryptographyService
    {
        Jwk GetKeys();
        Jwk GetPublicKey();
        string PrivateDecrypt(string token);
        string PublicEncrypt(string plainText);
    }
}