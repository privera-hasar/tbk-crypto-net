using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class HasarDecryptCommand : AbstractCommand
    {
        private readonly IJoseCryptographyService _cryptoService;

        public HasarDecryptCommand(IJoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public override void Run(string token)
        {
            WriteTitle("Extracting data from JWE token with Hasar private key");

            Console.WriteLine("JWE Token: " + token);
            Console.WriteLine();

            var decrypted = _cryptoService.HasarDecrypt(token);
            Console.WriteLine("Plain text: " + decrypted);
            Console.WriteLine();


        }
    }
}
