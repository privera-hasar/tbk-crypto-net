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

        public override void Run(string data)
        {
            WriteTitle("Creating JWE token with TBK public key");

            Console.WriteLine("Plain text: " + data);
            Console.WriteLine();

            var encrypted = _cryptoService.TbkEncrypt(data);
            Console.WriteLine("JWE Token: " + encrypted);
            Console.WriteLine();
        }
    }
}
