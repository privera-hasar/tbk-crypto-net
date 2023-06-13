using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class TbkDecryptCommand : AbstractCommand
    {
        private readonly IJoseCryptographyService _cryptoService;

        public TbkDecryptCommand(IJoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public override void Run(string token)
        {
            WriteTitle("Extracting data from JWE token with Tbk private key");

            Console.WriteLine("JWE Token: " + token);
            Console.WriteLine();

            var decrypted = _cryptoService.TbkDecrypt(token);
            Console.WriteLine("Plain text: " + decrypted);
            Console.WriteLine();
        }
    }
}
