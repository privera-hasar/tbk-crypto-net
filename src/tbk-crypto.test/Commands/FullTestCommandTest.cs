using Jose;
using Moq;
using Newtonsoft.Json.Linq;
using tbk_crypto.Commands;
using tbk_crypto.Services;

namespace tbk_crypto.test.Commands
{
    [TestClass]
    public class FullTestCommandTest
    {
        private readonly Mock<IJoseCryptographyService> _cryptoService = new();

        private FullTestCommand CreateSut()
        {
            return new FullTestCommand(_cryptoService.Object);
        }

        [TestMethod]
        public void Run()
        {
            string plainText = "unit-test";
            string hasarToken = "hasar-token";
            string tbkToken = "tbk-token";

            _cryptoService.Setup(x => x.HasarEncrypt(plainText)).Returns(hasarToken);
            _cryptoService.Setup(x => x.HasarDecrypt(hasarToken)).Returns(plainText);
            _cryptoService.Setup(x => x.TbkEncrypt(plainText)).Returns(tbkToken);
            _cryptoService.Setup(x => x.TbkDecrypt(tbkToken)).Returns(plainText);

            var sut = CreateSut();
            sut.Run(plainText);

            _cryptoService.Verify(x => x.HasarEncrypt(plainText), Times.Once());
            _cryptoService.Verify(x => x.HasarDecrypt(hasarToken), Times.Once());
            _cryptoService.Verify(x => x.TbkEncrypt(plainText), Times.Once());
            _cryptoService.Verify(x => x.TbkDecrypt(tbkToken), Times.Once());
        }
    }
}
