using Moq;
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
            string token = "token";

            _cryptoService.Setup(x => x.PublicEncrypt(plainText)).Returns(token);
            _cryptoService.Setup(x => x.PrivateDecrypt(token)).Returns(plainText);
            
            var sut = CreateSut();
            sut.Run(plainText);

            _cryptoService.Verify(x => x.PublicEncrypt(plainText), Times.Once());
            _cryptoService.Verify(x => x.PrivateDecrypt(token), Times.Once());
        }
    }
}
