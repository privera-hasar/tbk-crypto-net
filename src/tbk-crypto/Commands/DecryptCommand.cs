using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class DecryptCommand
    {
        private readonly JoseCryptographyService _cryptoService;

        public DecryptCommand(JoseCryptographyService cryptoService)
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

            var decrypted = _cryptoService.JosePublicDecrypt(data);
            Console.WriteLine("Decrypted data: " + decrypted);
            Console.WriteLine();


        }
    }
}
