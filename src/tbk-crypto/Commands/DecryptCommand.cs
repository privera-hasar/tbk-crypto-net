using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class DecryptCommand
    {
        private readonly IJoseCryptographyService _cryptoService;

        public DecryptCommand(IJoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public void Run(string data)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Decrypting data with public key");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            Console.WriteLine("Data: " + data);
            Console.WriteLine();

            var decrypted = _cryptoService.PrivateDecrypt(data);
            Console.WriteLine("Decrypted data: " + decrypted);
            Console.WriteLine();


        }
    }
}
