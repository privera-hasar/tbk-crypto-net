using tbk_crypto.Services;

namespace tbk_crypto.Commands
{
    public class FullTestCommand : AbstractCommand
    {
        private readonly IJoseCryptographyService _cryptoService;

        public FullTestCommand(IJoseCryptographyService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public override void Run(string data)
        {
            WriteTitle("Full test");

            Console.WriteLine("Plain text: " + data);
            Console.WriteLine();

            WriteTitle("Creating JWE token with Hasar public key");

            var hasarEncrypted = _cryptoService.HasarEncrypt(data);
            Console.WriteLine("JWE Token: " + hasarEncrypted);
            Console.WriteLine();

            WriteTitle("Extracting JWE token plain text with Hasar private key");

            var hasarDecrypted = _cryptoService.HasarDecrypt(hasarEncrypted);
            Console.WriteLine("Decrypted data: " + hasarDecrypted);
            Console.WriteLine();

            WriteTitle("Creating JWE token with TBK public key");

            var tbkEncrypted = _cryptoService.TbkEncrypt(data);
            Console.WriteLine("JWE Token: " + tbkEncrypted);
            Console.WriteLine();

            WriteTitle("Extracting JWE token plain text with TBK private key");

            var tbkDecrypted = _cryptoService.TbkDecrypt(tbkEncrypted);
            Console.WriteLine("Decrypted data: " + tbkDecrypted);
            Console.WriteLine();
        }
    }
}
