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

        public void Run(string token)
        {
            Console.WriteLine("================================================");
            Console.WriteLine("Extracting data from JWE token with public key");
            Console.WriteLine("================================================");
            Console.WriteLine();

            Console.WriteLine("JWE Token: " + token);
            Console.WriteLine();

            var decrypted = _cryptoService.PrivateDecrypt(token);
            Console.WriteLine("Plain text: " + decrypted);
            Console.WriteLine();


        }
    }
}
