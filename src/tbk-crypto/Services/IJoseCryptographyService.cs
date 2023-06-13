using Jose;

namespace tbk_crypto.Services
{
    public interface IJoseCryptographyService
    {
        Jwk GetHasarKeys();
        Jwk GetTbkPublicKey();
        string HasarEncrypt(string plainText);
        string HasarDecrypt(string token);
        string TbkEncrypt(string plainText);
        string TbkDecrypt(string token);
    }
}