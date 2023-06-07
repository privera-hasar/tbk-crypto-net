using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class FullTestCommand
    {
        private readonly IJoseCryptographyService _cryptoService;

        public FullTestCommand(IJoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public void Run(string data)
        {
            Console.WriteLine("================================================");
            Console.WriteLine("Full test");
            Console.WriteLine("================================================");
            Console.WriteLine();

            Console.WriteLine("Plain text: " + data);
            Console.WriteLine();

            Console.WriteLine("================================================");
            Console.WriteLine("Creating JWE token with public key");
            Console.WriteLine("================================================");
            Console.WriteLine();

            var publicKeyEncrypted = _cryptoService.PublicEncrypt(data);
            Console.WriteLine("JWE Token: " + publicKeyEncrypted);
            Console.WriteLine();

            Console.WriteLine("================================================");
            Console.WriteLine("Extracting JWE token plain text with private key");
            Console.WriteLine("================================================");
            Console.WriteLine();

            var publicKeyEncryptedPrivateDecrypted = _cryptoService.PrivateDecrypt(publicKeyEncrypted);
            Console.WriteLine("Decrypted data: " + publicKeyEncryptedPrivateDecrypted);
            Console.WriteLine();
        }
    }
}
