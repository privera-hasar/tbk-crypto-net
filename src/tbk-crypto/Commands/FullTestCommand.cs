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
            Console.WriteLine("===========================================");
            Console.WriteLine("Full test");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            Console.WriteLine("Data: " + data);
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Encrypting data with private key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            var privateKeyEncrypted = _cryptoService.PrivateEncrypt(data);
            Console.WriteLine("Encrypted data: " + privateKeyEncrypted);
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Decrypting data with private key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            var privateKeyEncryptedPrivateDecrypted = _cryptoService.PrivateDecrypt(privateKeyEncrypted);
            Console.WriteLine("Decrypted data: " + privateKeyEncryptedPrivateDecrypted);
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Decrypting data with public key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            var privateKeyEncryptedPublicDecrypted = _cryptoService.PublicDecrypt(privateKeyEncrypted);
            Console.WriteLine("Decrypted data: " + privateKeyEncryptedPublicDecrypted);
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Encrypting data with public key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            var publicKeyEncrypted = _cryptoService.PublicEncrypt(data);
            Console.WriteLine("Encrypted data: " + publicKeyEncrypted);
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Decrypting data with private key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            var publicKeyEncryptedPrivateDecrypted = _cryptoService.PrivateDecrypt(publicKeyEncrypted);
            Console.WriteLine("Decrypted data: " + publicKeyEncryptedPrivateDecrypted);
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Decrypting data with public key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            try
            {
                var publicKeyEncryptedPublicDecrypted = _cryptoService.PublicDecrypt(publicKeyEncrypted);
                Console.WriteLine("Decrypted data: " + publicKeyEncryptedPublicDecrypted);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo desencriptar con la llave pública.");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
