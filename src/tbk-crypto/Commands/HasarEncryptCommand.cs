using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class HasarEncryptCommand : AbstractCommand
    {
        private readonly IJoseCryptographyService _cryptoService;

        public HasarEncryptCommand(IJoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public override void Run(string data)
        {
            WriteTitle("Creating JWE token with Hasar public key");

            Console.WriteLine("Plain text: " + data);
            Console.WriteLine();

            var encrypted = _cryptoService.HasarEncrypt(data);
            Console.WriteLine("JWE Token: " + encrypted);
            Console.WriteLine();
        }
    }
}
