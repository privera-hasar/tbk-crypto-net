using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class EncryptCommand
    {
        private readonly JoseCryptographyService _cryptoService;

        public EncryptCommand(JoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public void Run(string data)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Encrypting data with public key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            Console.WriteLine("Data: " + data);
            Console.WriteLine();

            var encrypted = _cryptoService.JosePublicEncrypt(data);
            Console.WriteLine("Encrypted data: " + encrypted);
            Console.WriteLine();
        }
    }
}
