using Jose;

namespace tbk_crypto.Services
{
    public interface IJoseCryptographyService
    {
        Jwk GetKeys();
        Jwk GetPublicKey();
        string PrivateDecrypt(string data);
        string PrivateEncrypt(string data);
        string PublicDecrypt(string data);
        string PublicEncrypt(string data);
    }
}